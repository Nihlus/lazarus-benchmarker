using AdvancedDLSupport;
using AdvancedDLSupport.AOT;

namespace Lazarus.ADL
{
    [AOTType]
    [NativeSymbols(Prefix = "lz")]
    public interface ILazarus
    {
        void InvertMatrixByPtr(ref Matrix2 matrix);
        Matrix2 InvertMatrixByValue(Matrix2 matrix);
    }
}