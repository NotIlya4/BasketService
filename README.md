# BasketService
This is a REST API Service that provides CRUD endpoints for managing basket. See swagger for endpoints docs.

## Environment Variables
Service contains several environment variables that you can change to control its behavior:
- `BasketExpiresInDays` Controls timespan, in days, after which Redis will automatically delete a user's basket.

Also there are some variables that are not intended to be changed:
- `SeqUrl` Seq url.
- `Serilog` Serilog logging settings.
- `RedisConnectionString__Server` Redis url.
- `RedisConnectionString__Server` Redis database number.
