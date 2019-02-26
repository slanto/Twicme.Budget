using Twicme.Budget.Cli.Options;

namespace Twicme.Budget.Cli
{
    public interface IBudgetFactory
    {
        Budget Create();
    }
}