namespace TMC.Tools.CoreLib.Core.Models;

public class VersionIdentifier(int major, int minor, int revision) : IComparable<VersionIdentifier>
{
    public int Major { get; } = major;
    public int Minor { get; } = minor;
    public int Revision { get; } = revision;

    public static VersionIdentifier Parse(string version)
    {
        var trimmed = version.TrimStart('v', 'V');
        var parts = trimmed.Split('.');
        return new VersionIdentifier(
            parts.Length > 0 && int.TryParse(parts[0], out var major) ? major : 0,
            parts.Length > 1 && int.TryParse(parts[1], out var minor) ? minor : 0,
            parts.Length > 2 && int.TryParse(parts[2], out var revision) ? revision : 0
        );
    }

    public int CompareTo(VersionIdentifier? other)
    {
        if (other is null)
            return 1;
        if (Major != other.Major)
            return Major.CompareTo(other.Major);
        return Minor != other.Minor
            ? Minor.CompareTo(other.Minor)
            : Revision.CompareTo(other.Revision);
    }

    public override string ToString() => $"{Major}.{Minor}.{Revision}";
}
