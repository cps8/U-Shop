using UShop.Shared.IdGenerator;
using FreeSql.DataAnnotations;
using UShop.Services.User.Domain.Base;

namespace UShop.Services.User.Domain.AggregateRoots;

/// <summary>
/// 账号
/// </summary>
public class Account : IAggregateRoot, IDeleted, ICreated, ILastUpdated
{
    public long Id { get; private set; }
    public bool IsDeleted { get; private set; }
    public string Name { get; private set; }
    public string Password { get; private set; }
    public string? Phone { get; private set; }
    public string? Avatar { get; private set; }
    public bool Disabled { get; private set; }
    public DateTime? DisabledTime { get; private set; }
    public string? DisabledReason { get; private set; }
    public long CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long LastUpdatedBy { get; private set; }
    public DateTime LastUpdatedAt { get; private set; }

    private Account() { }
    private Account(long id, string name, string password)
    {
        Id = id;
        Name = name;
        Password = password;
    }

    /// <summary>
    /// 创建账号
    /// </summary>
    /// <param name="idGeneratorServicee">id生成服务</param>
    /// <param name="name">用户名</param>
    /// <param name="password">密码</param>
    /// <returns></returns>
    public static Account Generator(ISnowflakeIdGeneratorService idGeneratorServicee, string name, string password)
    {
        // 获取id
        long id = idGeneratorServicee.GenerateId();
        return new Account(id, name, password);
    }
    /// <summary>
    /// 禁用
    /// </summary>
    /// <param name="reason">禁用原因</param>
    public void Disable(string reason)
    {
        Disabled = true;
        DisabledTime = DateTime.Now;
        DisabledReason = reason;
    }

    public void Delete()
    {
        IsDeleted = true;
    }
}
