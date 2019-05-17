﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Composition;
using Microsoft.CodeAnalysis.Classification;
using Microsoft.CodeAnalysis.Editor.Shared.Utilities;
using Microsoft.CodeAnalysis.Host;
using Microsoft.CodeAnalysis.Host.Mef;

namespace Microsoft.CodeAnalysis.ExternalAccess.LiveShare.Classification
{
    [ExportLanguageServiceFactory(typeof(IClassificationService), StringConstants.CSharpLspLanguageName), Shared]
    internal class CSharpLspClassificationServiceFactory : ILanguageServiceFactory
    {
        public ILanguageService CreateLanguageService(HostLanguageServices languageServices)
        {
            return languageServices.GetOriginalLanguageService<IClassificationService>();
        }
    }

    [ExportLanguageServiceFactory(typeof(IClassificationService), StringConstants.VBLspLanguageName), Shared]
    internal class VBLspClassificationServiceFactory : ILanguageServiceFactory
    {
        public ILanguageService CreateLanguageService(HostLanguageServices languageServices)
        {
            return languageServices.GetOriginalLanguageService<IClassificationService>();
        }
    }

    [ExportLanguageServiceFactory(typeof(ISyntaxClassificationService), StringConstants.CSharpLspLanguageName), Shared]
    internal class CSharpLspEditorClassificationFactoryService : ILanguageServiceFactory
    {
        private readonly RoslynLSPClientServiceFactory _roslynLSPClientServiceFactory;
        private readonly ClassificationTypeMap _classificationTypeMap;
        private readonly IThreadingContext _threadingContext;

        [ImportingConstructor]
        public CSharpLspEditorClassificationFactoryService(RoslynLSPClientServiceFactory roslynLSPClientServiceFactory, ClassificationTypeMap classificationTypeMap, IThreadingContext threadingContext)
        {
            _roslynLSPClientServiceFactory = roslynLSPClientServiceFactory ?? throw new ArgumentNullException(nameof(roslynLSPClientServiceFactory));
            _classificationTypeMap = classificationTypeMap ?? throw new ArgumentNullException(nameof(classificationTypeMap));
            _threadingContext = threadingContext;
        }

        public ILanguageService CreateLanguageService(HostLanguageServices languageServices)
        {
            return new RoslynClassificationService(_roslynLSPClientServiceFactory, languageServices.GetOriginalLanguageService<ISyntaxClassificationService>(), _classificationTypeMap, _threadingContext);
        }
    }

    [ExportLanguageServiceFactory(typeof(ISyntaxClassificationService), StringConstants.VBLspLanguageName), Shared]
    internal class VBLspEditorClassificationFactoryService : ILanguageServiceFactory
    {
        private readonly RoslynLSPClientServiceFactory _roslynLSPClientServiceFactory;
        private readonly ClassificationTypeMap _classificationTypeMap;
        private readonly IThreadingContext _threadingContext;

        [ImportingConstructor]
        public VBLspEditorClassificationFactoryService(RoslynLSPClientServiceFactory roslynLSPClientServiceFactory, ClassificationTypeMap classificationTypeMap, IThreadingContext threadingContext)
        {
            _roslynLSPClientServiceFactory = roslynLSPClientServiceFactory ?? throw new ArgumentNullException(nameof(roslynLSPClientServiceFactory));
            _classificationTypeMap = classificationTypeMap ?? throw new ArgumentNullException(nameof(classificationTypeMap));
            _threadingContext = threadingContext;
        }

        public ILanguageService CreateLanguageService(HostLanguageServices languageServices)
        {
            return new RoslynClassificationService(_roslynLSPClientServiceFactory, languageServices.GetOriginalLanguageService<ISyntaxClassificationService>(), _classificationTypeMap, _threadingContext);
        }
    }
}
