namespace ClubDoorman.Test;

public class Tests
{
    [TestCase("привет", false, TestName = "CyrillicWord_NoLookAlikes")]
    [TestCase("приве7", false, TestName = "CyrillicWord_WithDigit")]
    [TestCase("вас3к", false, TestName = "CyrillicWord_WithMixedLookAlikes")]
    [TestCase("мирu", true, TestName = "MostlyCyrillicWord_WithLookAlikes")]
    [TestCase("Прuвет!", true, TestName = "MostlyCyrillicWord_WithLookAlikes2")]
    [TestCase("privет", false, TestName = "PartiallyCyrillicWord_WithLookAlikes")]
    [TestCase("hello", false, TestName = "PurelyNonCyrillicWord")]
    [TestCase("", false, TestName = "EmptyString")]
    [TestCase("д", false, TestName = "SingleCyrillicCharacter")]
    [TestCase("a", false, TestName = "SingleNonCyrillicCharacter")]
    [TestCase("ПриВет!", false, TestName = "CyrillicWord_WithPunctuation")]
    [TestCase("Ищy людeй в комaнду, прuятный заработоk, мuнuмум затpат по верменu, подpобностu в лuчку", true, TestName = "BigSentence")]
    [TestCase("Ищyлюдeйαπό", true, TestName = "Greek")]
    [TestCase("это русское слово и в нём есть", false, TestName = "Yo")]
    [TestCase("хей", false, TestName = "IKratkoe")]
    public void HasLookAlikeSymbols_Tests(string word, bool expectedResult)
    {
        var result = SimpleFilters.FindAllRussianWordsWithLookalikeSymbols(word);
        Assert.That(result.Count > 0, Is.EqualTo(expectedResult), string.Join(", ", result));
    }
}