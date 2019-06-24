using PatternRecognition.Templates;

namespace PatternRecognition.Interfaces
{
    class IData
    {
        int ClassId { get; set; }
        string ClassName { get; }
        Vector Values { get; set; }
    }
}
