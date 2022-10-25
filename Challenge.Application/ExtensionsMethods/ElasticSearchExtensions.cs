﻿using Challenge.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace Challenge.Application.ExtensionsMethods {
    public static class ElasticSearchExtensions {
        public static void AddElasticsearch(this IServiceCollection services, IConfiguration configuration) {
            
            var url = configuration["ELKConfiguration:Uri"];
            var defaultIndex = configuration["ELKConfiguration:index"];

            var settings = new ConnectionSettings(new Uri(url)).BasicAuthentication(string.Empty, string.Empty).PrettyJson().DefaultIndex(defaultIndex);
            AddDefaultMappings(settings);

            var client = new ElasticClient(settings);
            services.AddSingleton<IElasticClient>(client);
            CreateIndex(client, defaultIndex);
        }

        private static void AddDefaultMappings(ConnectionSettings settings) {
            settings.DefaultMappingFor<Permission>(m => m);
        }

        private static void CreateIndex(IElasticClient client, string indexName) {
            var createIndexResponse = client.Indices.Create(indexName,
                index => index.Map<Permission>(x => x.AutoMap())
            );
        }
    }
}
