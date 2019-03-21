# Homework 2 - FTI (Fast Ticket Interpreter)

Application Server: https://ftiapi.azurewebsites.net/

Azure Function: https://fti-conversion.azurewebsites.net/api/Function2?code=BLytKhxIwTXiT/ZWhte2dQzRxI54HwjHaNU/3B1a4TYKlKVU7EwJAg== 

Application Client:

- https://fticlient.azurewebsites.net/receipts - view receipts
- https://fticlient.azurewebsites.net/receipts/create - create receipts

## Description
------------------------

We want to create a retail application using google Pub/Sub api.
A publisher will write receipts in a topic in a json format.

Subscribers:

- A subscriber will read the tickets and index them in solar
- A subsciber will read the tickets and do a interpretation on it
- A subscriber will archive the tickets

Client will be a PWA. Using websockets data about tickets will be updated real time on the mobile app.
On the client we will be able to create ticket and receive a copy through a websocket. A Faas will convert the receipt in xml.

## Implementation details
---------------------------

### Models

The application is centered around receipts. We want the simplest receipt.

```json
{  
   "CustomerNumber":"123",
   "Items":[  
      {  
         "Description":"Milk",
         "Price":{  
            "Currency":"EUR",
            "Value":10.0
         }
      },
      {  
         "Description":"Egs",
         "Price":{  
            "Currency":"EUR",
            "Value":5.0
         }
      },
      {  
         "Description":"Honey",
         "Price":{  
            "Currency":"EUR",
            "Value":2.0
         }
      }
   ],
   "Total":{  
      "Currency":"EUR",
      "Value":17.0
   }
}
```

## WebSocket
-----------------------

WebSocket is a computer communications protocol, providing full-duplex communication channels over a single TCP connection. The WebSocket protocol was standardized by the IETF as RFC 6455 in 2011, and the WebSocket API in Web IDL is being standardized by the W3C.

### Cors
-----------------------
To use websocket we have to enable cors so we can receive requests to open the tcp connection from our client.

```c#
var corsBuilder = new CorsPolicyBuilder();
      corsBuilder.AllowAnyHeader();
      corsBuilder.AllowAnyMethod();
      corsBuilder.AllowAnyOrigin(); 
      corsBuilder.WithOrigins("http://localhost:6001", "https://fticlient.azurewebsites.net", "http://localhost:4200");
      corsBuilder.AllowCredentials();

      services.AddCors(options =>
      {
            options.AddPolicy("CorsPolicy", corsBuilder.Build());
      });
```

### Signal R

We use Signal R for opening a websocket and notifying the client when we receive a receipt.

Server
```c#
app.UseSignalR(routes => { routes.MapHub<NotifyHub>("/notify"); });
```

Client
```js
constructor() { 
   this.hubConnection = new HubConnectionBuilder().withUrl(environment.host + "/notify").build();
   this.hubConnection
   .start()
   .then(() => console.log('Connection started!'))
   .catch(err => console.log('Error while establishing connection :('));
}

this.notificationService.hubConnection.on('BroadcastMessage', (type: string, payload: string, id: string) => {
   this.conversionService.convertReceipt(new NotificationMessage(payload,"Xml", id)).subscribe(x =>{
      this.xmlMessages.push(new NotificationMessage(x.payload, x.type, x.id))
});
```
## Google Pub/Sub
--------------

### Description

Cloud Pub/Sub brings the flexibility and reliability of enterprise message-oriented middleware to the cloud. At the same time, Cloud Pub/Sub is a scalable, durable event ingestion and delivery system that serves as a foundation for modern stream analytics pipelines. By providing many-to-many, asynchronous messaging that decouples senders and receivers, it allows for secure and highly available communication among independently written applications. Cloud Pub/Sub delivers low-latency, durable messaging that helps developers quickly integrate systems hosted on the Google Cloud Platform and externally.

Connection was done using a client for c#

```ps1
Install-Package Google.Cloud.PubSub.V1 -Pre
```

To authenticate to the service we need a private key from the google platform and add the key in the env path:

```
GOOGLE_APPLICATION_CREDENTIALS = [Path to key]
```

After authentication we can use the services following the [C# documentation](https://googleapis.github.io/google-cloud-dotnet/docs/Google.Cloud.PubSub.V1/).

### Publishing receipts

```json
URL: https://localhost:5001/api/values

Method: POST

Body:
{  
   "CustomerNumber":"123",
   "Items":[  
      {  
         "Description":"Milk",
         "Price":{  
            "Currency":"EUR",
            "Value":10.0
         }
      },
      {  
         "Description":"Egs",
         "Price":{  
            "Currency":"EUR",
            "Value":5.0
         }
      },
      {  
         "Description":"Honey",
         "Price":{  
            "Currency":"EUR",
            "Value":2.0
         }
      }
   ],
   "Total":{  
      "Currency":"EUR",
      "Value":17.0
   }
}
```

### Starting the subscribers

- Archive Subscriber
- Index Subscriber
- Interpreter Subscriber

```
Execute the FTI.Subscribers -- This will launch 3 subscribers that will receive a copy of the published receipt. 
```

## Azure Fabric Services

Documentation : https://azure.microsoft.com/en-us/services/service-fabric/ 

We used azure fabric services for creating a statefull service that calculates the total earnings from all our sales. Being a statefull service it persists data, so each receipt we make we send to this service and we save the amount.

```c#
[HttpPost]
public async Task<IActionResult> Post([FromBody] Receipt value)
{
   IReliableDictionary<string, float> votesDictionary = await this.stateManager.GetOrAddAsync<IReliableDictionary<string, float>>("total");

   using (ITransaction tx = this.stateManager.CreateTransaction())
   {
         await votesDictionary.AddOrUpdateAsync(tx, "TotalValue", value.Total.Value, (key, oldvalue) => oldvalue + value.Total.Value);
         await tx.CommitAsync();
   }

   return new OkResult();
}
```

```c#
[HttpGet]
public async Task<ActionResult<IEnumerable<string>>> Get()
{
   CancellationToken ct = new CancellationToken();

   IReliableDictionary<string, float> votesDictionary = await this.stateManager.GetOrAddAsync<IReliableDictionary<string, float>>("total");

   using (ITransaction tx = this.stateManager.CreateTransaction())
   {
         Microsoft.ServiceFabric.Data.IAsyncEnumerable<KeyValuePair<string, float>> list = await votesDictionary.CreateEnumerableAsync(tx);

         Microsoft.ServiceFabric.Data.IAsyncEnumerator<KeyValuePair<string, float>> enumerator = list.GetAsyncEnumerator();

         List<KeyValuePair<string, float>> result = new List<KeyValuePair<string, float>>();

         while (await enumerator.MoveNextAsync(ct))
         {
            result.Add(enumerator.Current);
         }

         return Ok(result);
   }  
}
```
Cluster with 1 node where we deployed the financial service.

<img src="Documentation/FabricServicesCluster.png"
     alt="Cluster image"
     style="float: left; margin-right: 10px;" />

## Resources
-------------

- https://medium.com/@rukshandangalla/how-to-notify-your-angular-5-app-using-signalr-5e5aea2030b2 
- https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-2.2 