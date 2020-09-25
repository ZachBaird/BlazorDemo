namespace BlazorDemo.Services
{
    /// <summary>
    /// Class containing some faux methods to demonstrate dependency injection
    ///  with Blazor Wasm.
    /// </summary>
    public class MyDependency : IMyDependency
    {
        public int DoStuff(int a, int b)
        {
            return a + b;
        }
    }
}
