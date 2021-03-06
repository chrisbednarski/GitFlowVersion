using GitFlowVersion;
using NUnit.Framework;

[TestFixture]
public class NugetVersionBuilderTests
{

    [Test]
    public void Develop_branch()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   BranchType = BranchType.Develop,
                                   Version = new SemanticVersion
                                             {
                                                 PreReleasePartOne = 4,
                                                 Stability = Stability.Unstable
                                             }
                               };
        var nugetVersion = NugetVersionBuilder.GenerateNugetVersion(versionAndBranch);
        NuGet.SemanticVersion.Parse(nugetVersion);
        Assert.AreEqual("0.0.0-Unstable0004", nugetVersion);

    }

    [Test]
    public void Develop_branch_with_preReleaseTwo()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   BranchType = BranchType.Develop,
                                   Version = new SemanticVersion
                                             {
                                                 PreReleasePartOne = 4,
                                                 PreReleasePartTwo = 6,
                                                 Stability = Stability.Unstable
                                             }
                               };

        var nugetVersion = NugetVersionBuilder.GenerateNugetVersion(versionAndBranch);
        NuGet.SemanticVersion.Parse(nugetVersion);
        Assert.AreEqual("0.0.0-Unstable0004-0006", nugetVersion);
    }

    [Test]
    public void Release_branch()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   BranchType = BranchType.Release,
                                   Version = new SemanticVersion
                                             {
                                                 PreReleasePartOne = 4,
                                                 Stability = Stability.Beta,
                                             }
                               };

        var nugetVersion = NugetVersionBuilder.GenerateNugetVersion(versionAndBranch);
        NuGet.SemanticVersion.Parse(nugetVersion);
        Assert.AreEqual("0.0.0-Beta0004", nugetVersion);
    }

    [Test]
    public void Release_branch_with_preReleaseTwo()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   BranchType = BranchType.Release,
                                   Version = new SemanticVersion
                                             {
                                                 PreReleasePartOne = 4,
                                                 PreReleasePartTwo = 8,
                                                 Stability = Stability.Beta,
                                             }
                               };

        var nugetVersion = NugetVersionBuilder.GenerateNugetVersion(versionAndBranch);
        NuGet.SemanticVersion.Parse(nugetVersion);
        Assert.AreEqual("0.0.0-Beta0004-0008", nugetVersion);
    }

    [Test]
    public void Hotfix_branch()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   BranchType = BranchType.Hotfix,
                                   Version = new SemanticVersion
                                             {
                                                 Stability = Stability.Beta,
                                                 PreReleasePartOne = 4
                                             }
                               };

        var nugetVersion = NugetVersionBuilder.GenerateNugetVersion(versionAndBranch);
        NuGet.SemanticVersion.Parse(nugetVersion);
        Assert.AreEqual("0.0.0-Beta0004", nugetVersion);
    }

    [Test]
    public void Hotfix_branch_with_preReleaseTwo()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   BranchType = BranchType.Hotfix,
                                   Version = new SemanticVersion
                                             {
                                                 Stability = Stability.Beta,
                                                 PreReleasePartOne = 4,
                                                 PreReleasePartTwo = 7,
                                             }
                               };

        var nugetVersion = NugetVersionBuilder.GenerateNugetVersion(versionAndBranch);
        NuGet.SemanticVersion.Parse(nugetVersion);
        Assert.AreEqual("0.0.0-Beta0004-0007", nugetVersion);
    }

    [Test]
    public void Pull_branch()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   BranchType = BranchType.PullRequest,
                                   Version = new SemanticVersion
                                             {
                                                 Suffix = "1571",
                                                 PreReleasePartOne = 131231232, //ignored
                                                 PreReleasePartTwo = 131231232, //ignored
                                                 Stability = Stability.Unstable
                                             }
                               };

        var nugetVersion = NugetVersionBuilder.GenerateNugetVersion(versionAndBranch);
        NuGet.SemanticVersion.Parse(nugetVersion);
        Assert.AreEqual("0.0.0-PullRequest-1571", nugetVersion);
    }

    [Test]
    public void Feature_branch()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   BranchType = BranchType.Feature,
                                   Sha = "TheSha",
                                   BranchName = "AFeature",
                                   Version = new SemanticVersion
                                             {
                                                 PreReleasePartOne = 4, //ignored
                                                 PreReleasePartTwo = 4, //ignored
                                                 Stability = Stability.Unstable
                                             }
                               };

        var nugetVersion = NugetVersionBuilder.GenerateNugetVersion(versionAndBranch);
        NuGet.SemanticVersion.Parse(nugetVersion);
        Assert.AreEqual("0.0.0-Feature-AFeature-TheSha", nugetVersion);
    }

    [Test]
    public void Master_branch()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   Version = new SemanticVersion
                                             {
                                                 Stability = Stability.Final,
                                                 Suffix = "1571", //ignored
                                                 PreReleasePartOne = 131231232, //ignored
                                                 PreReleasePartTwo = 131231232 //ignored
                                             }
                               };

        var nugetVersion = NugetVersionBuilder.GenerateNugetVersion(versionAndBranch);
        NuGet.SemanticVersion.Parse(nugetVersion);
        Assert.AreEqual("0.0.0", nugetVersion);
    }

    [Test]
    public void NuGet_version_should_be_padded_to_workaround_stupid_nuget_issue_with_sorting_one_digit()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   BranchType = BranchType.Develop,
                                   Version = new SemanticVersion
                                             {
                                                 PreReleasePartOne = 4,
                                                 Stability = Stability.Unstable
                                             }
                               };
        var nugetVersion = NugetVersionBuilder.GenerateNugetVersion(versionAndBranch);
        NuGet.SemanticVersion.Parse(nugetVersion);
        Assert.AreEqual("0.0.0-Unstable0004", nugetVersion);
    }

    [Test]
    public void NuGet_version_should_be_padded_to_workaround_stupid_nuget_issue_with_sorting_one_digit_with_preReleaseTwo()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   BranchType = BranchType.Develop,
                                   Version = new SemanticVersion
                                             {
                                                 PreReleasePartOne = 4,
                                                 PreReleasePartTwo = 5,
                                                 Stability = Stability.Unstable
                                             }
                               };
        var nugetVersion = NugetVersionBuilder.GenerateNugetVersion(versionAndBranch);
        NuGet.SemanticVersion.Parse(nugetVersion);
        Assert.AreEqual("0.0.0-Unstable0004-0005", nugetVersion);
    }

    [Test]
    public void NuGet_version_should_be_padded_to_workaround_stupid_nuget_issue_with_sorting_two_digits()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   BranchType = BranchType.Develop,
                                   Version = new SemanticVersion
                                             {
                                                 PreReleasePartOne = 40,
                                                 Stability = Stability.Unstable
                                             }
                               };
        var nugetVersion = NugetVersionBuilder.GenerateNugetVersion(versionAndBranch);
        NuGet.SemanticVersion.Parse(nugetVersion);
        Assert.AreEqual("0.0.0-Unstable0040", nugetVersion);
    }

    [Test]
    public void NuGet_version_should_be_padded_to_workaround_stupid_nuget_issue_with_sorting_two_digits_with_preReleaseTwo()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   BranchType = BranchType.Develop,
                                   Version = new SemanticVersion
                                             {
                                                 PreReleasePartOne = 40,
                                                 PreReleasePartTwo = 50,
                                                 Stability = Stability.Unstable
                                             }
                               };
        var nugetVersion = NugetVersionBuilder.GenerateNugetVersion(versionAndBranch);
        NuGet.SemanticVersion.Parse(nugetVersion);
        Assert.AreEqual("0.0.0-Unstable0040-0050", nugetVersion);
    }

    [Test]
    public void NuGet_version_should_be_padded_to_workaround_stupid_nuget_issue_with_sorting_three_digits()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   BranchType = BranchType.Develop,
                                   Version = new SemanticVersion
                                             {
                                                 PreReleasePartOne = 400,
                                                 Stability = Stability.Unstable
                                             }
                               };
        var nugetVersion = NugetVersionBuilder.GenerateNugetVersion(versionAndBranch);
        NuGet.SemanticVersion.Parse(nugetVersion);
        Assert.AreEqual("0.0.0-Unstable0400", nugetVersion);
    }

    [Test]
    public void NuGet_version_should_be_padded_to_workaround_stupid_nuget_issue_with_sorting_three_digits_with_preReleaseTwo()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   BranchType = BranchType.Develop,
                                   Version = new SemanticVersion
                                             {
                                                 PreReleasePartOne = 400,
                                                 PreReleasePartTwo = 500,
                                                 Stability = Stability.Unstable
                                             }
                               };
        var nugetVersion = NugetVersionBuilder.GenerateNugetVersion(versionAndBranch);
        NuGet.SemanticVersion.Parse(nugetVersion);
        Assert.AreEqual("0.0.0-Unstable0400-0500", nugetVersion);
    }

    [Test]
    public void NuGet_version_should_be_padded_to_workaround_stupid_nuget_issue_with_sorting_four_digits()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   BranchType = BranchType.Develop,
                                   Version = new SemanticVersion
                                             {
                                                 PreReleasePartOne = 4000,
                                                 Stability = Stability.Unstable
                                             }
                               };
        var nugetVersion = NugetVersionBuilder.GenerateNugetVersion(versionAndBranch);
        NuGet.SemanticVersion.Parse(nugetVersion);
        Assert.AreEqual("0.0.0-Unstable4000", nugetVersion);
    }

    [Test]
    public void NuGet_version_should_be_padded_to_workaround_stupid_nuget_issue_with_sorting_four_digits_with_preReleaseTwo()
    {
        var versionAndBranch = new VersionAndBranch
                               {
                                   BranchType = BranchType.Develop,
                                   Version = new SemanticVersion
                                             {
                                                 PreReleasePartOne = 4000,
                                                 PreReleasePartTwo = 4000,
                                                 Stability = Stability.Unstable
                                             }
                               };
        var nugetVersion = NugetVersionBuilder.GenerateNugetVersion(versionAndBranch);
        NuGet.SemanticVersion.Parse(nugetVersion);
        Assert.AreEqual("0.0.0-Unstable4000-4000", nugetVersion);
    }
}