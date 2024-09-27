# TwoPhaseCommitProtocolExample
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
