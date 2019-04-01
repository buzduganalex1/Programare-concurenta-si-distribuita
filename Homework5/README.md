# Hyperledger presentation

## Description 
Hyperledger is a company developing Business Blockchain Frameworks and Tools that help developers to create blockchain applications.

Frameworks:

- Hyperledger Fabric : Permissioned with channel support
- Hyperledger Burrow : Permissionable smart contract machine
- Hyperledger Grid : WebAssembly based project for building supply chain sollutions
- Hyperledger Indy : Decentralized Identity
- Hyperledger Iroha : Mobile application focus
- Hyperledger SawTooth : Permissioned and permissionless support EVM transaction family.

Tools:

- Hyperledger Caliper : Blockchain framework benchmark platform
- Hyperledger Composer : Model and build blockchain networks
- Hyperledger Cello : As a service deployment
- Hyperledger Explorer : View and explore data on the blockchain
- Hyperledger Quilt : Ledger interoperability
- Hyperledger Ursa : Shared Cryptographic Library

## Points of interest

### Hyperledger Fabric(https://hyperledger-fabric.readthedocs.io/en/release-1.4/)
-------------------
Hyperledger Fabric is a platform for distributed ledger solutions underpinned by a modular architecture delivering high degrees of confidentiality, resiliency, flexibility, and scalability. It is designed to support pluggable implementations of different components and accommodate the complexity and intricacies that exist across the economic ecosystem.

#### Key concepts

- Blockchain

    At the heart of a blockchain network is a distributed ledger that records all the transactions that take place on the network.

    A blockchain ledger is often described as decentralized because it is replicated across many network participants, each of whom collaborate in its maintenance. We’ll see that decentralization and collaboration are powerful attributes that mirror the way businesses exchange goods and services in the real world.

    In addition to being decentralized and collaborative, the information recorded to a blockchain is append-only, using cryptographic techniques that guarantee that once a transaction has been added to the ledger it cannot be modified.

    his property of “immutability” makes it simple to determine the provenance of information because participants can be sure information has not been changed after the fact. It’s why blockchains are sometimes described as systems of proof.

- Smart Contract / Chaincode

    To support the consistent update of information — and to enable a whole host of ledger functions (transacting, querying, etc) — a blockchain network uses smart contracts to provide controlled access to the ledger.

    Smart contracts are not only a key mechanism for encapsulating information and keeping it simple across the network, they can also be written to allow participants to execute certain aspects of transactions automatically.

- Consensus

    The process of keeping the ledger transactions synchronized across the network — to ensure that ledgers update only when transactions are approved by the appropriate participants, and that when ledgers do update, they update with the same transactions in the same order — is called consensus.

- Shared Ledger

    Hyperledger Fabric has a ledger subsystem comprising two components: the __world state__ and the __transaction log__. Each participant has a copy of the ledger to every Hyperledger Fabric network they belong to.

    The world state component describes the state of the ledger at a given point in time. It’s the database of the ledger. The transaction log component records all transactions which have resulted in the current value of the world state; it’s the update history for the world state. The ledger, then, is a combination of the world state database and the transaction log history.

Hyperledger Fabric is one of the blockchain projects within Hyperledger. Like other blockchain technologies, it has a ledger, uses smart contracts, and is a system by which participants manage their transactions.

Where Hyperledger Fabric breaks from some other blockchain systems is that it is private and permissioned. Rather than an open permissionless system that allows unknown identities to participate in the network (requiring protocols like “proof of work” to validate transactions and secure the network), the members of a Hyperledger Fabric network enroll through a trusted Membership Service Provider (MSP).

Hyperledger fabric capabilities:

https://docs.google.com/presentation/d/1sM18O4OJASJH_QdXBvqwxRzGSdO-RqDjXDqpDKGraOE/edit#slide=id.g3b1d3e2847_0_113 

### Hyperledger Indy (https://github.com/hyperledger/indy-node#how-to-contribute0)
--------------------

### Hyperledger Composer (https://hyperledger.github.io/composer/latest/)

Hyperledger Composer is an extensive, open development toolset and framework to make developing blockchain applications easier. Our primary goal is to accelerate time to value, and make it easier to integrate your blockchain applications with the existing business systems. You can use Composer to rapidly develop use cases and deploy a blockchain solution in weeks rather than months. Composer allows you to model your business network and integrate existing systems and data with your blockchain applications.

How does it work in practice?

For an example of a business network in action; a realtor can quickly model their business network as such:

- __Assets__: houses and listings
- __Participants__: buyers and homeowners
- __Transactions__: buying or selling houses, and creating and closing listings

Participants can have their access to transactions restricted based on their role as either a buyer, seller, or realtor. The realtor can then create an application to present buyers and sellers with a simple user interface for viewing open listings and making offers. This business network could also be integrated with existing inventory system, adding new houses as assets and removing sold properties. Relevant other parties can be registered as participants, for example a land registry might interact with a buyer to transfer ownership of the land.