using Microsoft.Azure.SqlDatabase.ElasticScale.ShardManagement;

namespace AmazingShop.Shared.Core.Model
{
    public class Sharding
    {
        public ShardMapManager ShardMapManager { get; private set; }
        public ListShardMap<byte[]> ShardMap { get; private set; }
        public Sharding(string shardManagerDatabase, string connectionString)
        {
            ShardMapManager shardMapManager;
            if (!ShardMapManagerFactory.TryGetSqlShardMapManager(connectionString, ShardMapManagerLoadPolicy.Lazy, out shardMapManager))
            {
                this.ShardMapManager = ShardMapManagerFactory.CreateSqlShardMapManager(connectionString);
            }
            else
            {
                this.ShardMapManager = shardMapManager;
            }
            ListShardMap<byte[]> shardMap;
            if (!ShardMapManager.TryGetListShardMap(shardManagerDatabase, out shardMap))
            {
                this.ShardMap = ShardMapManager.CreateListShardMap<byte[]>(shardManagerDatabase);
            }
            else
            {
                this.ShardMap = shardMap;
            }
        }
        public void RegisterNewShard(byte[] key, string server, string database)
        {
            Shard shard;
            ShardLocation shardLocation = new ShardLocation(server, database, SqlProtocol.Tcp, 1433);
            if (!this.ShardMap.TryGetShard(shardLocation, out shard))
            {
                shard = this.ShardMap.CreateShard(shardLocation);
            }
            if (!this.ShardMap.TryGetMappingForKey(key, out _))
            {
                this.ShardMap.CreatePointMapping(key, shard);
            }
        }
    }
}