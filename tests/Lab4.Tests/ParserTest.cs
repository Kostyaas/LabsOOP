using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Implementation;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Parsing;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Parsing.DTO;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab4.Tests;

public class ParserTest
{
    private readonly CommandParser _parser;
    private readonly CommandFactory _factory;

    public ParserTest()
    {
        _factory = new CommandFactory();
        _parser = new CommandParser(_factory);
    }

    [Fact]
    public void Parse_ConnectCommand_WithAddress_ReturnsAddressParameter()
    {
        string input = "connect C:\\test";

        ParsedCommand? parsedCommand = _parser.Parse(input);

        Assert.NotNull(parsedCommand);
        ConnectCommand connectCommand = Assert.IsType<ConnectCommand>(parsedCommand.Command);
        Assert.Contains("Address", connectCommand.Pattern);
        Assert.Equal("C:\\test", parsedCommand.Parameters["Address"]);
    }

    [Fact]
    public void Parse_ConnectCommand_WithModeFlag_OverridesDefault()
    {
        string input = "connect C:\\test -m remote";

        ParsedCommand? parsedCommand = _parser.Parse(input);

        Assert.NotNull(parsedCommand);
        Assert.Equal("C:\\test", parsedCommand.Parameters["Address"]);
        Assert.Equal("remote", parsedCommand.Flags["-m"]); // Переопределено
    }

    [Fact]
    public void Parse_TreeListCommand_WithDepth_ReturnsDepthFlag()
    {
        string input = "tree list -d 3";

        ParsedCommand? parsedCommand = _parser.Parse(input);

        Assert.NotNull(parsedCommand);
        TreeListCommand treeListCommand = Assert.IsType<TreeListCommand>(parsedCommand.Command);
        Assert.Equal("3", parsedCommand.Flags["-d"]);
        Assert.Empty(parsedCommand.Parameters);
    }

    [Fact]
    public void Parse_FileShowCommand_ReturnsPathAndMode()
    {
        string input = "file show document.txt -m console";

        ParsedCommand? parsedCommand = _parser.Parse(input);

        Assert.NotNull(parsedCommand);
        FileShowCommand fileShowCommand = Assert.IsType<FileShowCommand>(parsedCommand.Command);
        Assert.Contains("Path", fileShowCommand.Pattern);
        Assert.Equal("document.txt", parsedCommand.Parameters["Path"]);
        Assert.Equal("console", parsedCommand.Flags["-m"]);
    }

    [Fact]
    public void Parse_FileShowCommand_WithoutMode_ReturnsDefaultConsole()
    {
        string input = "file show document.txt";

        ParsedCommand? parsedCommand = _parser.Parse(input);

        Assert.NotNull(parsedCommand);
        Assert.Equal("document.txt", parsedCommand.Parameters["Path"]);
    }

    [Fact]
    public void Parse_TreeGotoCommand_ReturnsPathParameter()
    {
        string input = "tree goto folder";

        ParsedCommand? parsedCommand = _parser.Parse(input);

        Assert.NotNull(parsedCommand);
        TreeGotoCommand treeGotoCommand = Assert.IsType<TreeGotoCommand>(parsedCommand.Command);
        Assert.Contains("Path", treeGotoCommand.Pattern);
        Assert.Equal("folder", parsedCommand.Parameters["Path"]);
    }

    [Fact]
    public void Parse_FileCopyCommand_ReturnsSourceAndDestination()
    {
        string input = "file copy source.txt dest.txt";

        ParsedCommand? parsedCommand = _parser.Parse(input);

        Assert.NotNull(parsedCommand);
        FileCopyCommand fileCopyCommand = Assert.IsType<FileCopyCommand>(parsedCommand.Command);
        Assert.Contains("SourcePath", fileCopyCommand.Pattern);
        Assert.Contains("DestinationPath", fileCopyCommand.Pattern);
        Assert.Equal("source.txt", parsedCommand.Parameters["SourcePath"]);
        Assert.Equal("dest.txt", parsedCommand.Parameters["DestinationPath"]);
    }

    [Fact]
    public void Parse_DisconnectCommand_ReturnsNoParameters()
    {
        string input = "disconnect";

        ParsedCommand? parsedCommand = _parser.Parse(input);

        Assert.NotNull(parsedCommand);
        DisconnectCommand disconnectCommand = Assert.IsType<DisconnectCommand>(parsedCommand.Command);
        Assert.Empty(disconnectCommand.Pattern);
        Assert.Empty(parsedCommand.Parameters);
        Assert.Empty(parsedCommand.Flags);
    }
}