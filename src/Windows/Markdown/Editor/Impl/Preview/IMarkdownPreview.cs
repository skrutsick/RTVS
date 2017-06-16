﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Microsoft.Markdown.Editor.Preview {
    internal interface IMarkdownPreview {
        void Update();
        void RunCurrentChunk();
        void RunAllChunksAbove();
    }
}
