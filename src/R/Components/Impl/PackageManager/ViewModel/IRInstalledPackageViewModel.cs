// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Microsoft.R.Components.PackageManager.ViewModel {
    public interface IRInstalledPackageViewModel : IRPackageViewModel {
        string Description { get; }
        string[] Authors { get; }
        string Url { get; }
        string Repository { get; }
        string BuildRVersion { get; }
    }
}