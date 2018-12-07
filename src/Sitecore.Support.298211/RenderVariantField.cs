namespace Sitecore.Support.XA.Foundation.RenderingVariants.Pipelines.RenderVariantField
{
  using Sitecore.Data.Fields;
  using Sitecore.Data.Items;
  using Sitecore.XA.Foundation.RenderingVariants.Fields;
  using Sitecore.XA.Foundation.Variants.Abstractions.Fields;
  using System.Linq;

  public class RenderVariantField : Sitecore.XA.Foundation.RenderingVariants.Pipelines.RenderVariantField.RenderVariantField
  {
    protected override BaseVariantField ResolveFallback(VariantField variantField, Item item, bool isControlEditable)
    {
      if (variantField.FallbackFields.Any())
      {
        foreach (BaseVariantField fallbackField in variantField.FallbackFields)
        {
          if (!(fallbackField is VariantField))
          {
            return fallbackField;
          }
          VariantField variantField2 = fallbackField as VariantField;
          bool flag = variantField2.UseFieldRenderer && PageMode.IsExperienceEditorEditing;
          Field field = item.Fields[variantField2.FieldName];
          if (field != null)
          {
            if (flag & isControlEditable)
            {
              return variantField2;
            }
            string value = field.GetValue(true);
            if (!string.IsNullOrWhiteSpace(value) && value != "$name")
            {
              return variantField2;
            }
          }
        }
      }
      return variantField;
    }
  }
}