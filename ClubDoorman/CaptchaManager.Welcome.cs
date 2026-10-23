using System.Net;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ClubDoorman;

internal partial class CaptchaManager
{
    private async Task SendWelcomeMessage(User user, Chat chat)
    {
        if (_config.WelcomeChatId != chat.Id)
            return;

        var name = WebUtility.HtmlEncode(Utils.FullName(user));
        await _bot.SendMessage(
            chat.Id,
            $@"Добро пожаловать, <a href=""tg://user?id={user.Id}"">{name}</a>!
<b>Капча пройдена!</b>
• <b>Расписание пробежек:</b> ищи в закрепе, если не готово - через поиск по слову <i>таймтейбл</i> можно примерно понять.
• <b>Как присоединиться:</b> приходи к точке старта к нужному времени. Переживаешь за темп? Напиши в чат, на какую пробежку хочешь — найдём компанию.
• <b>Вещи:</b> обычно можно оставить на старте (проверяй трек! если совпадает с финишем)
• <b>Трек:</b> @NakarteGpxBot автоматически переводит ссылки с nakarte в GPX.",
            parseMode: ParseMode.Html,
            ephemeralMessageParameters: new EphemeralMessageParameters { ReceiverUserId = user.Id }
        );
    }
}
