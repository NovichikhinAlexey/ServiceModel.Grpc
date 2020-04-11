﻿using Microsoft.Extensions.Logging;

namespace ServiceModel.Grpc.AspNetCore.Internal
{
    internal sealed class LogAdapter : ILogger
    {
        private readonly Microsoft.Extensions.Logging.ILogger _logger;

        public LogAdapter(Microsoft.Extensions.Logging.ILogger logger)
        {
            _logger = logger;
        }

        public void LogError(string message, params object[] args) => _logger.LogError(message, args);
    }
}