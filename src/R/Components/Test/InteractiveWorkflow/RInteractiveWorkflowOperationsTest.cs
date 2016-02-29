﻿using System.ComponentModel.Composition.Hosting;
using System.Threading.Tasks;
using Microsoft.R.Components.InteractiveWorkflow;
using Microsoft.UnitTests.Core.Threading;
using Xunit;

namespace Microsoft.R.Components.Test.InteractiveWorkflow {
    public class RInteractiveWorkflowOperationsTest : IAsyncLifetime  {
        private readonly ExportProvider _exportProvider;
        private readonly IRInteractiveWorkflow _workflow;
        private IInteractiveWindowVisualComponent _workflowComponent;

        public RInteractiveWorkflowOperationsTest(RComponentsMefCatalogFixture catalog) {
            _exportProvider = catalog.CreateExportProvider();
            _workflow = _exportProvider.GetExportedValue<IRInteractiveWorkflowProvider>().GetOrCreate();
        }

        public async Task InitializeAsync() {
            var componentFactory = _exportProvider.GetExportedValue<IInteractiveWindowComponentContainerFactory>();
            _workflowComponent = await UIThreadHelper.Instance.Invoke(() => _workflow.GetOrCreateVisualComponent(componentFactory));
        }

        public Task DisposeAsync() {
            _workflowComponent.Dispose();
            return Task.CompletedTask;
        }
    }
}
