﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using Microsoft.Common.Core.Disposables;
using Microsoft.Common.Core.Shell;
using Microsoft.Common.Wpf;

namespace Microsoft.R.Components.ConnectionManager.Implementation.ViewModel {
    internal abstract class ConnectionStatusBaseViewModel : BindableBase, IDisposable {
        private readonly DisposableBag _disposableBag;

        private bool _isRemote;
        private bool _isActive;
        private bool _isConnected;
        private bool _isRunning;

        protected ICoreShell Shell { get; }
        protected IConnectionManager ConnectionManager { get; }

        public ConnectionStatusBaseViewModel(IConnectionManager connectionManager, ICoreShell shell) {
            ConnectionManager = connectionManager;
            Shell = shell;

            _disposableBag = DisposableBag.Create<ConnectionStatusBarViewModel>()
                .Add(() => connectionManager.ConnectionStateChanged -= ConnectionStateChanged);

            connectionManager.ConnectionStateChanged += ConnectionStateChanged;
            IsConnected = connectionManager.IsConnected;
            IsRunning = connectionManager.IsRunning;
        }

        public virtual void Dispose(bool disposing) {
            _disposableBag.TryDispose();
        }

        public void Dispose() {
            Dispose(true);
        }

        protected virtual void ConnectionStateChanged() { }

        private void ConnectionStateChanged(object sender, EventArgs e) {
            Shell.DispatchOnUIThread(() => {
                IsConnected = ConnectionManager.IsConnected;
                IsRunning = ConnectionManager.IsConnected && ConnectionManager.IsRunning;
                IsActive = ConnectionManager.ActiveConnection != null;
                IsRemote = ConnectionManager.ActiveConnection?.IsRemote ?? false;

                ConnectionStateChanged();
            });
        }

        public bool IsConnected {
            get { return _isConnected; }
            set { SetProperty(ref _isConnected, value); }
        }

        public bool IsRunning {
            get { return _isRunning; }
            set { SetProperty(ref _isRunning, value); }
        }

        public bool IsActive {
            get { return _isActive; }
            set { SetProperty(ref _isActive, value); }
        }

        public bool IsRemote {
            get { return _isRemote; }
            set { SetProperty(ref _isRemote, value); }
        }
    }
}
