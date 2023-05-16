# BasketService
This is a REST API Service for YetAnotherMarketplace that provides CRUD endpoints for managing basket. See swagger for endpoints docs.

## Environment Variables
Service contains several environment variables that you can change to control its behavior:
- `BasketExpiresInDays` Controls timespan, in days, after which Redis will automatically delete a user's basket.
- `SeqUrl` Seq url for logging.
- `Serilog` Override serilog configuration.
- `RedisConnectionString__Server` Redis url.
- `RedisConnectionString__Server` Redis database number.
