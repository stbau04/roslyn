// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#nullable disable

using Microsoft.CodeAnalysis.CSharp.Test.Utilities;
using Roslyn.Test.Utilities;
using Xunit;

namespace Microsoft.CodeAnalysis.CSharp.UnitTests.CodeGen
{
    public class PatternTests : EmitMetadataTestBase
    {
        [Fact]
        public void RefParameter_StoreToTemp()
        {
            var source =
@"public class C
{
    static bool M1(ref object x)
    {
        return x is 42;
    }
}";
            var compilation = CreateCompilation(source, options: TestOptions.ReleaseDll);
            compilation.VerifyDiagnostics();
            var compVerifier = CompileAndVerify(compilation);
            compVerifier.VerifyIL("C.M1",
@"{
  // Code size       24 (0x18)
  .maxstack  2
  .locals init (object V_0)
  IL_0000:  nop
  IL_0001:  ldarg.0
  IL_0002:  ldind.ref
  IL_0003:  stloc.0
  IL_0004:  ldloc.0
  IL_0005:  isinst     ""int""
  IL_000a:  brfalse.s  IL_0016
  IL_000c:  ldloc.0
  IL_000d:  unbox.any  ""int""
  IL_0011:  ldc.i4.s   42
  IL_0013:  ceq
  IL_0015:  ret
  IL_0016:  ldc.i4.0
  IL_0017:  ret
}");
        }
    }
}
