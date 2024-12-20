// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Linq.Expressions;

namespace Microsoft.CSharp.RuntimeBinder.ComInterop
{
    [RequiresUnreferencedCode(Binder.TrimmerWarning)]
    internal sealed class ComClassMetaObject : DynamicMetaObject
    {
        internal ComClassMetaObject(Expression expression, ComTypeClassDesc cls)
            : base(expression, BindingRestrictions.Empty, cls)
        {
        }

        public override DynamicMetaObject BindCreateInstance(CreateInstanceBinder binder, DynamicMetaObject[] args)
        {
            return new DynamicMetaObject(
                Expression.Call(
                    Helpers.Convert(Expression, typeof(ComTypeClassDesc)),
                    typeof(ComTypeClassDesc).GetMethod(nameof(ComTypeClassDesc.CreateInstance))
                ),
                BindingRestrictions.Combine(args).Merge(
                    BindingRestrictions.GetTypeRestriction(Expression, typeof(ComTypeClassDesc))
                )
            );
        }
    }
}
