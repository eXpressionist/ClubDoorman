# Ветка side

Общие файлы сборки и развёртывания берём из `main`, настройки форка храним отдельно:

- `.github/workflows/dotnet.yml` — workflow из `main` без изменений.
- `.github/workflows/dotnet-side.yml` — сборка `side` и публикация образа форка.
  Версия SDK берётся из `ClubDoorman/ClubDoorman.csproj`, поэтому переход `main` на новую .NET подхватывается автоматически.
- `docker-compose.yml.example` — общий compose из `main`.
- `docker-compose.side.yml` — override с образом форка, каталогом данных и дополнительными переменными.
- `ClubDoorman/CaptchaManager.Welcome.cs` — текст приветствия после капчи.

## Развёртывание

Из корня репозитория, с настройками в `.env`:

```sh
docker compose --env-file .env -f docker-compose.yml.example -f docker-compose.side.yml pull
docker compose --env-file .env -f docker-compose.yml.example -f docker-compose.side.yml up -d
```

Дополнительные настройки `side`:

| Переменная | Назначение |
| --- | --- |
| `DOORMAN_CHANNELS_IGNORE_REPORT` | ID каналов через запятую, сообщения которых не нужно репортить как «сообщение от канала». Остальные проверки действуют. |
| `DOORMAN_WELCOME_CHAT_ID` | ID чата для личного приветствия про пробежки после успешной капчи; без настройки приветствие отключено. |

## Обновление из main

`.gitattributes` и `.editorconfig` задают LF, как в `main`. При первом обновлении после перехода `main` на LF
использован `git merge -Xrenormalize`: Git нормализовал общий предок перед сравнением файлов.
Переход сохранён в merge-коммите; дальше достаточно обычного слияния:

```sh
git fetch upstream
git merge upstream/main
```

Для `main` на коммите `549708e` обычное слияние проверено без специальных настроек Git.
Будущие изменения одной и той же логики в обеих ветках всё ещё могут потребовать разрешения конфликтов.
Правила «всегда брать side» не используются: они могли бы незаметно отбрасывать обновления `main`.
