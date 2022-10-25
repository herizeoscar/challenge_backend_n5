using Challenge.Application.DTOs;
using Challenge.Application.ExtensionsMethods;
using Challenge.Application.Services.Abstractions;
using Confluent.Kafka;
using Newtonsoft.Json;
using static Challenge.Application.Enums.ApplicationEnums;

namespace Challenge.Application.Services {
    public class KafkaService : IKafkaService {
        
        private readonly ProducerConfig config;

        public KafkaService(ProducerConfig config) {
            this.config = config;
        }

        public async Task RegisterOperation(string topic, Operation operation) {
            using(var producer = new ProducerBuilder<Null, string>(config).Build()) {
                await producer.ProduceAsync(topic, new Message<Null, string> {
                    Value = JsonConvert.SerializeObject(new KafkaMessageDto() {
                        Id = Guid.NewGuid(),
                        NameOperation = operation.ToDescription()
                    })
                });
            }
        }

    }
}
