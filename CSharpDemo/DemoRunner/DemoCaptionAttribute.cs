namespace CSharpDemo.DemoRunner;

public class DemoCaptionAttribute : Attribute
{
    public string Caption { get; set; }

    public DemoCaptionAttribute(string caption) => this.Caption = caption;
}