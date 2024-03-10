using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.RichTextEditor;

namespace CompanyPortal.Constants;

public class DataGrid
{
    public static List<ItemModel> ToolbarItems(string gridId) =>
    [
        new ItemModel
        {
            Text = "Xuất Excel",
            Id = $"{gridId}_excelexport",
            PrefixIcon = "fa-solid fa-file-excel"
        },
        new ItemModel
        {
            Text = "Xuất PDF",
            Id = $"{gridId}_pdfexport",
            PrefixIcon = "fa-solid fa-file-pdf"
        }
    ];

    public static List<ToolbarItemModel> ContentEditorTools =
    [
        new ToolbarItemModel { Command = ToolbarCommand.Bold },
        new ToolbarItemModel { Command = ToolbarCommand.Italic },
        new ToolbarItemModel { Command = ToolbarCommand.Underline },
        new ToolbarItemModel { Command = ToolbarCommand.Separator },
        new ToolbarItemModel { Command = ToolbarCommand.Formats },
        new ToolbarItemModel { Command = ToolbarCommand.Alignments },
        new ToolbarItemModel { Command = ToolbarCommand.UnorderedList },
        new ToolbarItemModel { Command = ToolbarCommand.Separator },
        new ToolbarItemModel { Command = ToolbarCommand.CreateLink },
        new ToolbarItemModel { Command = ToolbarCommand.Image },
        new ToolbarItemModel { Command = ToolbarCommand.Video },
        new ToolbarItemModel { Command = ToolbarCommand.CreateTable },
        new ToolbarItemModel { Command = ToolbarCommand.Separator }
    ];
}
