# TwoPhaseCommitProtocolExample

#### Introduction

The **Two-Phase Commit (2PC)** protocol is a mechanism used in distributed systems to ensure that transactions are completed atomically. This is particularly crucial when transactions span multiple data sources, guaranteeing that all operations either succeed entirely or fail entirely, maintaining data consistency across the system.

#### What is the Two-Phase Commit Protocol?

The Two-Phase Commit protocol is a process executed in two stages to safely complete a transaction between a **coordinator** and one or more **participants**. It consists of the following two phases:

1. **Prepare Phase**: 
   - The coordinator asks all participants if they are ready to commit the transaction. Each participant checks its local transaction state and responds with one of the following:
     - **Ready**: The participant can commit.
     - **Not Ready**: The participant cannot commit.
   - If all participants reply that they are ready, the coordinator proceeds to the next phase.

2. **Commit Phase**:
   - After ensuring that all participants are ready, the coordinator sends a commit command, instructing them to finalize the transaction. If any participant cannot commit or a failure occurs, the coordinator sends an abort command to rollback the transaction.
   - Participants then execute the given command (commit or abort) based on the coordinator's decision.

#### Key Terminology

- **Coordinator**: The entity that manages the Two-Phase Commit protocol. It sends commands to the participants and makes the final decision (commit or abort).
- **Participant**: Nodes involved in the transaction that follow the coordinator’s commands to either commit or rollback.
- **Prepare Phase**: The phase where the coordinator asks participants if they are ready to commit.
- **Commit Phase**: The phase where, after ensuring readiness, the transaction is finalized by either committing or aborting.
- **Commit**: The process of successfully completing a transaction, making the changes permanent.
- **Abort**: The process of rolling back a transaction, undoing any changes made during the process.

#### Use Cases of Two-Phase Commit

The Two-Phase Commit protocol is often used to maintain data consistency in distributed systems, especially in scenarios such as:

- **Distributed Databases**: Ensuring consistency across databases located on different servers.
- **Microservice Architectures**: Coordinating transactions across multiple services.
- **Message Queuing Systems**: Ensuring reliable message processing and storage.

#### Advantages and Disadvantages

**Advantages**:
- Guarantees data consistency in distributed systems.
- Ensures that a transaction is atomic, meaning it is either fully successful or fully rolled back.

**Disadvantages**:
- **High Latency**: The process of waiting for all participants to respond can introduce delays.
- **Coordinator Failure**: If the coordinator fails, participants may remain in a waiting state indefinitely.
- **Resource Intensive**: Participants must lock resources until the commit or abort command is received.

#### Project Structure

This project simulates a system using the Two-Phase Commit protocol, where:
- A **coordinator** service manages the transaction and communicates with participants.
- Multiple **participants** either complete or rollback the transaction based on the coordinator’s instructions.
- Delays and failures are considered in the simulation to demonstrate real-world scenarios.


4. The project will run locally and simulate the distributed system.

#### Conclusion

The Two-Phase Commit Protocol is a robust method for ensuring safe transaction completion in distributed systems. This project provides a basic example of how the protocol operates and can be used as a learning tool for understanding the intricacies of distributed transaction management.


### Results Examples
#### Pending state example

![image](https://github.com/user-attachments/assets/00dacb39-68dd-4d16-84c1-2706b50a671b)


#### successfully state example
![image](https://github.com/user-attachments/assets/d668560d-2d60-4a7e-8818-29aa15416c42)


#### Abort state example
![image](https://github.com/user-attachments/assets/0171957d-0620-4385-879b-77e835107926)

####  Source
[Gençay Yıldız](https://www.gencayyildiz.com/blog/)

```c#
public interface ITransactionService
{
    Task<Guid> CreateTransactionAsync();
    Task PrepareServicesAsync(Guid transactionId);
    Task<bool> CheckReadyServicesAsync(Guid transactionId);
    Task CommitAsync(Guid transactionId);
    Task<bool> CheckTransactionStateServicesAsync(Guid transactionId);
    Task RollbackAsync(Guid transactionId);
}
```


![image](https://github.com/user-attachments/assets/0b47b7f2-d3da-48f2-adff-4214f51e8cec)

![image](https://github.com/user-attachments/assets/c3003677-2711-4709-8695-34db2145ff0a)

![image](https://github.com/user-attachments/assets/cfece541-5696-470d-969f-c3a6a3c0c5cc)
![image](https://github.com/user-attachments/assets/bfe73dca-3415-45ab-b367-943ffb42f89b)
![image](https://github.com/user-attachments/assets/2f963ced-bdfe-4c02-b004-7fcf551c6ae9)
![image](https://github.com/user-attachments/assets/aa826df4-b706-48fe-a862-9105dbe23bc2)
