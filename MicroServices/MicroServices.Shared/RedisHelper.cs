using CSRedis;

namespace SmartConference.Api.Filter
{
    public class RedisHelp<T>
    {
        CSRedisClient redis;

        public RedisHelp(CSRedisClient redis)
        {
            this.redis = redis;
        }

        public async Task<List<T>> GetRedisList(string key, Func<Task<List<T>>> func, int time)
        {
            var redisinfo=await  redis.GetAsync<List<T>>(key);
            if (redisinfo != null)
            {
                return redisinfo;
            }
            var list = await func();
            await redis.SetAsync(key, list, time);
            return list;
        }
    }
}
