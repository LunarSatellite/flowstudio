namespace Aurora.FlowStudio.Entity.Enums
{
    public enum ContentFormat
    {
        Text,
        PlainText,
        Html,
        Markdown,
        Json
    }

    public enum FormFieldType
    {
        Text,
        Email,
        Phone,
        Number,
        Date,
        Select,
        Checkbox,
        Radio,
        TextArea
    }

    public enum TemplateStatus
    {
        Draft,
        Active,
        Archived,
        Deprecated
    }

    public enum TemplateType
    {
        Text,
        Simple,
        Rich,
        Interactive,
        Form,
        Receipt
    }
}
