namespace Common.Enums;

[Flags]
public enum AccountStatus
{
    Disabled = 1 << 4,    // 已停用
    Deleted = 1 << 3,     // 已刪除
    Unverified = 1 << 2,  // 未驗證
    Verified = 1 << 1    // 已驗證
}
