# VeloMotoAPI

VeloMotoAPI - это RESTful API для управления магазином велосипедов и мотоциклов.
## Технологии

- ASP.NET Core 6.0
- Entity Framework Core
- Redis (для кэширования)
- AutoMapper
- SQL Server
- Swagger/OpenAPI

## Основные функции

- CRUD операции для продуктов, категорий и производителей
- Кэширование данных с использованием Redis
- Поиск и фильтрация продуктов
- Управление ценами продуктов
- Валидация данных
- Глобальная обработка ошибок
- Структурированное логирование

## Архитектура

Проект следует принципам чистой архитектуры и включает следующие слои:

- Controllers - обработка HTTP запросов
- Services - бизнес-логика
- DataAccess - работа с базой данных
- Models - модели данных и DTO
- Middleware - промежуточное ПО

## Начало работы

### Предварительные требования

- .NET 8.0
- SQL Server
- Redis Server

### Конфигурация

Основные настройки находятся в файле `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=VeloMotoDB;Trusted_Connection=True;MultipleActiveResultSets=true",
    "Redis": "localhost:6379"
  }
}
```

## API Endpoints

### Продукты
- GET /api/products - получить все продукты
- GET /api/products/{id} - получить продукт по ID
- POST /api/products - создать новый продукт
- PUT /api/products - обновить продукт
- DELETE /api/products/{id} - удалить продукт
- GET /api/products/search - поиск продуктов
- GET /api/products/filter - фильтрация продуктов

### Категории
- GET /api/categories - получить все категории
- GET /api/categories/{id} - получить категорию по ID
- POST /api/categories - создать новую категорию
- PUT /api/categories - обновить категорию
- DELETE /api/categories/{id} - удалить категорию

### Производители
- GET /api/manufacturers - получить всех производителей
- GET /api/manufacturers/{id} - получить производителя по ID
- POST /api/manufacturers - создать нового производителя
- PUT /api/manufacturers - обновить производителя
- DELETE /api/manufacturers/{id} - удалить производителя

## Кэширование

В проекте реализовано кэширование с использованием Redis:

- Кэширование результатов запросов
- Автоматическая инвалидация кэша при изменении данных
- Настраиваемое время жизни кэша
- Префиксы для разных типов данных

## Логирование

Настроено структурированное логирование с использованием различных провайдеров:

- Console logging
- Debug logging
- JSON logging (в режиме разработки)
- Event Source logging

## Обработка ошибок

Реализована глобальная обработка ошибок с помощью middleware:

- Стандартизированные ответы об ошибках
- Логирование исключений
- Различные уровни детализации ошибок для разных окружений

## Безопасность

- CORS настройки для frontend приложения
- Identity для аутентификации и авторизации
- Безопасное хранение конфигурации 