using Microsoft.CodeAnalysis.CSharp.Test.Utilities;
using Roslyn.Test.Utilities;
using Xunit;

namespace Microsoft.CodeAnalysis.CSharp.UnitTests.CodeGen
{
    public class PatternTests_New : CSharpTestBase
    {
        [Fact]
        public void RefNullable_IsPattern_NoTemp()
        {
            var source = @"class C
{
    static bool Test4(ref int? v) => v is { };
}";

            var verifier = CompileAndVerify(source);
            verifier.VerifyIL("C.Test4",
@"{
  // Code size        7 (0x7)
  .maxstack  1
  IL_0000:  ldarg.0
  IL_0001:  call       ""bool int?.HasValue.get""
  IL_0006:  ret
}");
        }
    }
}
