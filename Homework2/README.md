# Homework 2 

FTI (Fast Ticket Interpreter)

## Server

We want to create a retail application using google Pub/Sub api.

A publisher will write receipts in a topic in a json format.

Subscribers:

- A subscriber will read the tickets and index them in solar
- A subsciber will read the tickets and do a interpretation on it
- A subscriber will archive the tickets

## Client

Client will be a PWA.

Using websockets data about tickets will be updated real time on the mobile app.

# Implementation details

## Models

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

## Google Pub/Sub

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
