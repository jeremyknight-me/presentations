using Microsoft.Playwright;

namespace TestingWebApps.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    [Test]
    public async Task Amount100ShouldExpectDefault25PercentToBe125Total()
    {
        _ = await this.Page.GotoAsync("https://tipcalc.jeremyknight.me");

        // Expect a title "to contain" a substring.
        await this.Expect(this.Page).ToHaveTitleAsync(new Regex("TipCalc"));

        // create a locator
        var amountInput = this.Page.Locator("id=bill-amount");
        await this.Expect(amountInput).ToHaveValueAsync("");
        await amountInput.FillAsync("100.00");

        var customPercent = this.Page.Locator("id=custom-percent");
        await this.Expect(customPercent).ToHaveValueAsync("25");

        var customTipDisplays = this.Page.Locator("css=tr#custom-tip-row td");

        var customTip = customTipDisplays.Nth(1);
        await this.Expect(customTip).ToHaveTextAsync("$25.00");

        var customTotal = customTipDisplays.Nth(2);
        await this.Expect(customTotal).ToHaveTextAsync("$125.00");
    }

    [Test]
    public async Task Amount125RoundUpResetRoundDown()
    {
        _ = await this.Page.GotoAsync("https://tipcalc.jeremyknight.me");

        await this.Page.GetByLabel("Amount").FillAsync("125.00");
        await this.Page.GetByRole(AriaRole.Button, new() { Name = "Round  Up" }).ClickAsync();

        var roundUpDisplays = this.Page.Locator("css=tr#custom-tip-row td");
        await this.Expect(roundUpDisplays.Nth(1)).ToHaveTextAsync("$31.00");
        await this.Expect(roundUpDisplays.Nth(2)).ToHaveTextAsync("$156.00");

        await this.Page.GetByRole(AriaRole.Button, new() { Name = "Reset " }).ClickAsync();

        var resetDisplays = this.Page.Locator("css=tr#custom-tip-row td");
        await this.Expect(resetDisplays.Nth(1)).ToHaveTextAsync("$31.25");
        await this.Expect(resetDisplays.Nth(2)).ToHaveTextAsync("$156.25");

        await this.Page.GetByRole(AriaRole.Button, new() { Name = "Round  Down" }).ClickAsync();

        var roundDownDisplays = this.Page.Locator("css=tr#custom-tip-row td");
        await this.Expect(roundDownDisplays.Nth(1)).ToHaveTextAsync("$32.00");
        await this.Expect(roundDownDisplays.Nth(2)).ToHaveTextAsync("$157.00");
    }
}
