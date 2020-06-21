﻿// <copyright>
// Copyright 2020 Max Ieremenko
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

using System;
using ServiceModel.Grpc.Configuration;
using ServiceModel.Grpc.Interceptors;

//// ReSharper disable CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
//// ReSharper restore CheckNamespace
{
    /// <summary>
    /// Provides a default configuration for all ServiceModel.Grpc services.
    /// </summary>
    public sealed class ServiceModelGrpcServiceOptions
    {
        /// <summary>
        /// Gets or sets default factory for serializing and deserializing messages.
        /// </summary>
        public IMarshallerFactory? DefaultMarshallerFactory { get; set; }

        /// <summary>
        /// Gets or sets default factory for server call error handler.
        /// </summary>
        public Func<IServiceProvider, IServerErrorHandler>? DefaultErrorHandlerFactory { get; set; }
    }
}
