using static Challenge.Application.Enums.ApplicationEnums;

namespace Challenge.Application.Services.Abstractions {
    public interface IKafkaService {

        Task RegisterOperation(string topic, Operation operation);

    }
}
