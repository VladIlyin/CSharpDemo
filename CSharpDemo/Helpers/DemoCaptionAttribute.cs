namespace CSharpDemo.Helpers;

public class DemoCaptionAttribute : Attribute
{
    public string Caption { get; set; }

    public DemoCaptionAttribute(string caption) => this.Caption = caption;
}