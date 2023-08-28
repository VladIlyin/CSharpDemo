using CSharpDemo.DemoRunner;

namespace CSharpDemo.Demos.PatternMatching;

// Switch expression - combination of switch and expression bodied member
// Always returns a value
public class SwitchExpressionDemo : DemoRunner<SwitchExpressionDemo>
{
    interface IProvider { }

    class SmsProvider : IProvider
    { 
        public string PhoneNumber { get; set; }
        public bool IsConsent { get; set; }
    }
    class EmailProvider : IProvider { public string Email { get; set; } }
    class DeliveryProvider : IProvider { public string Address { get; set; } } 
    class UnknownProvider : IProvider { } 

    [DemoCaption("Pattern matching: switch expression, type pattern")]
    public void Demo1()
    {
        string GetTo(IProvider deliveryProvider) =>
            deliveryProvider switch
            {
                SmsProvider { IsConsent: true } provider => provider.PhoneNumber,
                SmsProvider => "Disallow to send message",
                EmailProvider provider => provider.Email,
                DeliveryProvider provider => provider.Address,
                _ => "No provider",
            };

        IProvider smsProviderWithConsent = new SmsProvider() { PhoneNumber = "+1123456789", IsConsent = true };
        IProvider smsProviderNoConsent = new SmsProvider() { PhoneNumber = "+1123456789" };
        IProvider unknownProvider = new UnknownProvider() { };

        Console.WriteLine(GetTo(smsProviderWithConsent));
        Console.WriteLine(GetTo(smsProviderNoConsent));
        Console.WriteLine(GetTo(unknownProvider));
    }

    [DemoCaption("Pattern matching: ")]
    public void Demo2()
    {
        //while (fast != null && fast.Next != null)
        //while (fast is { Next: { } })
    }
}