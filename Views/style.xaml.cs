<Application xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="ProjectBReady.App">
  <Application.Resources>
    <ResourceDictionary>

      <!-- ══════════════════════════════════════════
           DESIGN TOKENS  —  matched 1:1 to style.css :root
      ══════════════════════════════════════════ -->
      <Color x:Key="SidebarBg">#0F1724</Color>
      <Color x:Key="SidebarHover">#1A2538</Color>
      <Color x:Key="SidebarActive">#1E2D45</Color>
      <Color x:Key="MainBg">#F0F4F8</Color>
      <Color x:Key="CardBg">#FFFFFF</Color>
      <Color x:Key="TableHeaderBg">#F8FAFC</Color>
      <Color x:Key="ToolbarBg">#FAFBFC</Color>

      <Color x:Key="Accent">#F97316</Color>
      <Color x:Key="AccentLight">#FFF4ED</Color>
      <Color x:Key="AccentDark">#C05C0A</Color>

      <Color x:Key="Teal">#0D9488</Color>
      <Color x:Key="TealLight">#F0FDFA</Color>
      <Color x:Key="Blue">#2563EB</Color>
      <Color x:Key="BlueLight">#EFF6FF</Color>
      <Color x:Key="Red">#EF4444</Color>
      <Color x:Key="RedLight">#FEF2F2</Color>
      <Color x:Key="Amber">#F59E0B</Color>
      <Color x:Key="AmberLight">#FFFBEB</Color>
      <Color x:Key="Green">#10B981</Color>
      <Color x:Key="GreenLight">#ECFDF5</Color>

      <Color x:Key="TextPrimary">#0F172A</Color>
      <Color x:Key="TextSecondary">#475569</Color>
      <Color x:Key="TextMuted">#94A3B8</Color>
      <Color x:Key="Border">#E2E8F0</Color>
      <Color x:Key="BorderStrong">#CBD5E1</Color>

      <!-- Badge text colours -->
      <Color x:Key="BadgeGreenText">#065F46</Color>
      <Color x:Key="BadgeRedText">#991B1B</Color>
      <Color x:Key="BadgeAmberText">#92400E</Color>
      <Color x:Key="BadgeTealText">#065E58</Color>
      <Color x:Key="BadgeBlueText">#1E40AF</Color>

      <!-- Row highlight backgrounds -->
      <Color x:Key="RowExpiredBg">#FFF5F5</Color>
      <Color x:Key="RowExpiringBg">#FFFBEB</Color>
      <Color x:Key="RowFullBg">#FFF5F5</Color>

      <!-- ══════════════════════════════════════════
           CARD  (.card / .stat-card / .kpi-card)
      ══════════════════════════════════════════ -->
      <Style x:Key="Card" TargetType="Frame">
        <Setter Property="BackgroundColor" Value="{StaticResource CardBg}"/>
        <Setter Property="BorderColor"     Value="{StaticResource Border}"/>
        <Setter Property="CornerRadius"    Value="16"/>
        <Setter Property="HasShadow"       Value="False"/>
        <Setter Property="Padding"         Value="0"/>
        <Setter Property="Margin"          Value="0,0,0,20"/>
      </Style>

      <Style x:Key="StatCard" TargetType="Frame">
        <Setter Property="BackgroundColor" Value="{StaticResource CardBg}"/>
        <Setter Property="BorderColor"     Value="{StaticResource Border}"/>
        <Setter Property="CornerRadius"    Value="16"/>
        <Setter Property="HasShadow"       Value="False"/>
        <Setter Property="Padding"         Value="24,22"/>
      </Style>

      <Style x:Key="KpiCard" TargetType="Frame">
        <Setter Property="BackgroundColor" Value="{StaticResource CardBg}"/>
        <Setter Property="BorderColor"     Value="{StaticResource Border}"/>
        <Setter Property="CornerRadius"    Value="16"/>
        <Setter Property="HasShadow"       Value="False"/>
        <Setter Property="Padding"         Value="22,18"/>
      </Style>

      <!-- ══════════════════════════════════════════
           TYPOGRAPHY
      ══════════════════════════════════════════ -->
      <Style x:Key="PageH1" TargetType="Label">
        <Setter Property="FontSize"       Value="22"/>
        <Setter Property="FontAttributes" Value="Bold"/>
        <Setter Property="TextColor"      Value="{StaticResource TextPrimary}"/>
      </Style>

      <Style x:Key="PageSubtitle" TargetType="Label">
        <Setter Property="FontSize"  Value="13"/>
        <Setter Property="TextColor" Value="{StaticResource TextSecondary}"/>
        <Setter Property="Margin"    Value="0,2,0,0"/>
      </Style>

      <Style x:Key="CardTitle" TargetType="Label">
        <Setter Property="FontSize"       Value="15"/>
        <Setter Property="FontAttributes" Value="Bold"/>
        <Setter Property="TextColor"      Value="{StaticResource TextPrimary}"/>
      </Style>

      <Style x:Key="CardSubtitle" TargetType="Label">
        <Setter Property="FontSize"  Value="12"/>
        <Setter Property="TextColor" Value="{StaticResource TextMuted}"/>
        <Setter Property="Margin"    Value="0,2,0,0"/>
      </Style>

      <Style x:Key="StatValue" TargetType="Label">
        <Setter Property="FontSize"       Value="32"/>
        <Setter Property="FontAttributes" Value="Bold"/>
        <Setter Property="TextColor"      Value="{StaticResource TextPrimary}"/>
        <Setter Property="LineHeight"     Value="1"/>
      </Style>

      <Style x:Key="StatLabel" TargetType="Label">
        <Setter Property="FontSize"       Value="13"/>
        <Setter Property="FontAttributes" Value="Bold"/>
        <Setter Property="TextColor"      Value="{StaticResource TextSecondary}"/>
        <Setter Property="Margin"         Value="0,6,0,0"/>
      </Style>

      <Style x:Key="StatSub" TargetType="Label">
        <Setter Property="FontSize"  Value="12"/>
        <Setter Property="TextColor" Value="{StaticResource TextMuted}"/>
        <Setter Property="Margin"    Value="0,3,0,0"/>
      </Style>

      <Style x:Key="KpiValue" TargetType="Label">
        <Setter Property="FontSize"       Value="28"/>
        <Setter Property="FontAttributes" Value="Bold"/>
        <Setter Property="TextColor"      Value="{StaticResource TextPrimary}"/>
      </Style>

      <Style x:Key="KpiLabel" TargetType="Label">
        <Setter Property="FontSize"         Value="11"/>
        <Setter Property="FontAttributes"   Value="Bold"/>
        <Setter Property="TextColor"        Value="{StaticResource TextMuted}"/>
        <Setter Property="CharacterSpacing" Value="1"/>
      </Style>

      <Style x:Key="KpiNote" TargetType="Label">
        <Setter Property="FontSize"  Value="12"/>
        <Setter Property="TextColor" Value="{StaticResource TextSecondary}"/>
        <Setter Property="Margin"    Value="0,4,0,0"/>
      </Style>

      <!-- Table -->
      <Style x:Key="TableHeader" TargetType="Label">
        <Setter Property="FontSize"         Value="11"/>
        <Setter Property="FontAttributes"   Value="Bold"/>
        <Setter Property="TextColor"        Value="{StaticResource TextMuted}"/>
        <Setter Property="CharacterSpacing" Value="1"/>
        <Setter Property="VerticalOptions"  Value="Center"/>
      </Style>

      <Style x:Key="TableCell" TargetType="Label">
        <Setter Property="FontSize"        Value="13"/>
        <Setter Property="TextColor"       Value="{StaticResource TextPrimary}"/>
        <Setter Property="VerticalOptions" Value="Center"/>
        <Setter Property="Padding"         Value="0,13"/>
      </Style>

      <Style x:Key="TableCellMuted" TargetType="Label" BasedOn="{StaticResource TableCell}">
        <Setter Property="TextColor" Value="{StaticResource TextSecondary}"/>
      </Style>

      <Style x:Key="TableCellBold" TargetType="Label" BasedOn="{StaticResource TableCell}">
        <Setter Property="FontAttributes" Value="Bold"/>
      </Style>

      <!-- Nav -->
      <Style x:Key="NavSectionLabel" TargetType="Label">
        <Setter Property="FontSize"         Value="10"/>
        <Setter Property="FontAttributes"   Value="Bold"/>
        <Setter Property="TextColor"        Value="{StaticResource TextMuted}"/>
        <Setter Property="CharacterSpacing" Value="1.5"/>
        <Setter Property="Margin"           Value="12,10,0,4"/>
      </Style>

      <Style x:Key="AdminText" TargetType="Label">
        <Setter Property="FontSize"       Value="12"/>
        <Setter Property="FontAttributes" Value="Bold"/>
        <Setter Property="TextColor"      Value="{StaticResource Green}"/>
      </Style>

      <Style x:Key="AdminHint" TargetType="Label">
        <Setter Property="FontSize"  Value="10"/>
        <Setter Property="TextColor" Value="{StaticResource TextMuted}"/>
        <Setter Property="Margin"    Value="0,1,0,0"/>
      </Style>

      <!-- Section divider label -->
      <Style x:Key="SectionDivider" TargetType="Label">
        <Setter Property="FontSize"         Value="12"/>
        <Setter Property="FontAttributes"   Value="Bold"/>
        <Setter Property="TextColor"        Value="{StaticResource TextMuted}"/>
        <Setter Property="CharacterSpacing" Value="1.5"/>
        <Setter Property="Margin"           Value="0,8,0,0"/>
      </Style>

      <!-- Form -->
      <Style x:Key="FormLabel" TargetType="Label">
        <Setter Property="FontSize"       Value="12"/>
        <Setter Property="FontAttributes" Value="Bold"/>
        <Setter Property="TextColor"      Value="{StaticResource TextSecondary}"/>
        <Setter Property="Margin"         Value="0,0,0,6"/>
      </Style>

      <Style x:Key="FormSectionTitle" TargetType="Label">
        <Setter Property="FontSize"         Value="12"/>
        <Setter Property="FontAttributes"   Value="Bold"/>
        <Setter Property="CharacterSpacing" Value="1"/>
        <Setter Property="TextColor"        Value="{StaticResource TextMuted}"/>
        <Setter Property="Margin"           Value="0,0,0,14"/>
      </Style>

      <!-- Modal -->
      <Style x:Key="ModalTitle" TargetType="Label">
        <Setter Property="FontSize"       Value="18"/>
        <Setter Property="FontAttributes" Value="Bold"/>
        <Setter Property="TextColor"      Value="{StaticResource TextPrimary}"/>
      </Style>

      <Style x:Key="ModalSub" TargetType="Label">
        <Setter Property="FontSize"  Value="12"/>
        <Setter Property="TextColor" Value="{StaticResource TextMuted}"/>
        <Setter Property="Margin"    Value="0,3,0,0"/>
      </Style>

      <!-- ══════════════════════════════════════════
           SIDEBAR BUTTONS  (.nav-item / .nav-item.active)
      ══════════════════════════════════════════ -->
      <Style x:Key="NavItem" TargetType="Button">
        <Setter Property="BackgroundColor"         Value="Transparent"/>
        <Setter Property="TextColor"               Value="#94A3B8"/>
        <Setter Property="FontSize"                Value="14"/>
        <Setter Property="FontAttributes"          Value="None"/>
        <Setter Property="HeightRequest"           Value="44"/>
        <Setter Property="HorizontalOptions"       Value="Fill"/>
        <Setter Property="HorizontalTextAlignment" Value="Start"/>
        <Setter Property="CornerRadius"            Value="8"/>
        <Setter Property="Padding"                 Value="12,0"/>
        <Setter Property="Margin"                  Value="0,1"/>
      </Style>

      <Style x:Key="NavItemActive" TargetType="Button" BasedOn="{StaticResource NavItem}">
        <Setter Property="BackgroundColor" Value="{StaticResource SidebarActive}"/>
        <Setter Property="TextColor"       Value="White"/>
        <Setter Property="FontAttributes"  Value="Bold"/>
      </Style>

      <!-- ══════════════════════════════════════════
           ACTION BUTTONS  (.btn-*)
      ══════════════════════════════════════════ -->
      <Style x:Key="BtnPrimary" TargetType="Button">
        <Setter Property="BackgroundColor" Value="{StaticResource Accent}"/>
        <Setter Property="TextColor"       Value="White"/>
        <Setter Property="FontAttributes"  Value="Bold"/>
        <Setter Property="FontSize"        Value="13"/>
        <Setter Property="CornerRadius"    Value="8"/>
        <Setter Property="Padding"         Value="16,9"/>
      </Style>

      <Style x:Key="BtnGhost" TargetType="Button">
        <Setter Property="BackgroundColor" Value="Transparent"/>
        <Setter Property="TextColor"       Value="{StaticResource TextSecondary}"/>
        <Setter Property="BorderColor"     Value="{StaticResource Border}"/>
        <Setter Property="BorderWidth"     Value="1"/>
        <Setter Property="FontSize"        Value="13"/>
        <Setter Property="CornerRadius"    Value="8"/>
        <Setter Property="Padding"         Value="16,9"/>
      </Style>

      <Style x:Key="BtnGhostSm" TargetType="Button" BasedOn="{StaticResource BtnGhost}">
        <Setter Property="FontSize" Value="12"/>
        <Setter Property="Padding"  Value="10,5"/>
      </Style>

      <Style x:Key="BtnGhostSmDanger" TargetType="Button" BasedOn="{StaticResource BtnGhostSm}">
        <Setter Property="TextColor"   Value="{StaticResource Red}"/>
        <Setter Property="BorderColor" Value="{StaticResource Red}"/>
      </Style>

      <Style x:Key="BtnDanger" TargetType="Button">
        <Setter Property="BackgroundColor" Value="{StaticResource Red}"/>
        <Setter Property="TextColor"       Value="White"/>
        <Setter Property="FontAttributes"  Value="Bold"/>
        <Setter Property="FontSize"        Value="13"/>
        <Setter Property="CornerRadius"    Value="8"/>
        <Setter Property="Padding"         Value="16,9"/>
      </Style>

      <Style x:Key="BtnTeal" TargetType="Button">
        <Setter Property="BackgroundColor" Value="{StaticResource Teal}"/>
        <Setter Property="TextColor"       Value="White"/>
        <Setter Property="FontAttributes"  Value="Bold"/>
        <Setter Property="FontSize"        Value="13"/>
        <Setter Property="CornerRadius"    Value="8"/>
        <Setter Property="Padding"         Value="16,9"/>
      </Style>

      <Style x:Key="BtnTealSm" TargetType="Button" BasedOn="{StaticResource BtnTeal}">
        <Setter Property="FontSize" Value="12"/>
        <Setter Property="Padding"  Value="10,5"/>
      </Style>

      <!-- ══════════════════════════════════════════
           BADGES  (.badge .badge-*)
      ══════════════════════════════════════════ -->
      <Style x:Key="BadgeBase" TargetType="Frame">
        <Setter Property="CornerRadius"      Value="20"/>
        <Setter Property="HasShadow"         Value="False"/>
        <Setter Property="Padding"           Value="10,3"/>
        <Setter Property="HorizontalOptions" Value="Start"/>
        <Setter Property="BorderColor"       Value="Transparent"/>
      </Style>

      <Style x:Key="BadgeGreen" TargetType="Frame" BasedOn="{StaticResource BadgeBase}">
        <Setter Property="BackgroundColor" Value="{StaticResource GreenLight}"/>
      </Style>
      <Style x:Key="BadgeRed" TargetType="Frame" BasedOn="{StaticResource BadgeBase}">
        <Setter Property="BackgroundColor" Value="{StaticResource RedLight}"/>
      </Style>
      <Style x:Key="BadgeAmber" TargetType="Frame" BasedOn="{StaticResource BadgeBase}">
        <Setter Property="BackgroundColor" Value="{StaticResource AmberLight}"/>
      </Style>
      <Style x:Key="BadgeTeal" TargetType="Frame" BasedOn="{StaticResource BadgeBase}">
        <Setter Property="BackgroundColor" Value="{StaticResource TealLight}"/>
      </Style>
      <Style x:Key="BadgeBlue" TargetType="Frame" BasedOn="{StaticResource BadgeBase}">
        <Setter Property="BackgroundColor" Value="{StaticResource BlueLight}"/>
      </Style>
      <Style x:Key="BadgeGray" TargetType="Frame" BasedOn="{StaticResource BadgeBase}">
        <Setter Property="BackgroundColor" Value="#F1F5F9"/>
      </Style>

      <Style x:Key="BadgeLabelGreen" TargetType="Label">
        <Setter Property="FontSize"       Value="11"/>
        <Setter Property="FontAttributes" Value="Bold"/>
        <Setter Property="TextColor"      Value="{StaticResource BadgeGreenText}"/>
      </Style>
      <Style x:Key="BadgeLabelRed" TargetType="Label">
        <Setter Property="FontSize"       Value="11"/>
        <Setter Property="FontAttributes" Value="Bold"/>
        <Setter Property="TextColor"      Value="{StaticResource BadgeRedText}"/>
      </Style>
      <Style x:Key="BadgeLabelAmber" TargetType="Label">
        <Setter Property="FontSize"       Value="11"/>
        <Setter Property="FontAttributes" Value="Bold"/>
        <Setter Property="TextColor"      Value="{StaticResource BadgeAmberText}"/>
      </Style>
      <Style x:Key="BadgeLabelTeal" TargetType="Label">
        <Setter Property="FontSize"       Value="11"/>
        <Setter Property="FontAttributes" Value="Bold"/>
        <Setter Property="TextColor"      Value="{StaticResource BadgeTealText}"/>
      </Style>

      <!-- ══════════════════════════════════════════
           ALERT BANNERS  (.alert-warn / .alert-danger)
      ══════════════════════════════════════════ -->
      <Style x:Key="AlertWarn" TargetType="Frame">
        <Setter Property="BackgroundColor" Value="#FFFBEB"/>
        <Setter Property="BorderColor"     Value="#FDE68A"/>
        <Setter Property="CornerRadius"    Value="12"/>
        <Setter Property="HasShadow"       Value="False"/>
        <Setter Property="Padding"         Value="18,13"/>
      </Style>

      <Style x:Key="AlertDanger" TargetType="Frame">
        <Setter Property="BackgroundColor" Value="{StaticResource RedLight}"/>
        <Setter Property="BorderColor"     Value="#FECACA"/>
        <Setter Property="CornerRadius"    Value="12"/>
        <Setter Property="HasShadow"       Value="False"/>
        <Setter Property="Padding"         Value="18,13"/>
      </Style>

      <!-- ══════════════════════════════════════════
           FORM FRAMES
      ══════════════════════════════════════════ -->
      <Style x:Key="FormCard" TargetType="Frame">
        <Setter Property="BackgroundColor" Value="{StaticResource CardBg}"/>
        <Setter Property="BorderColor"     Value="{StaticResource Border}"/>
        <Setter Property="CornerRadius"    Value="16"/>
        <Setter Property="HasShadow"       Value="False"/>
        <Setter Property="Padding"         Value="0"/>
        <Setter Property="MaximumWidthRequest" Value="560"/>
      </Style>

      <Style x:Key="FormInputFrame" TargetType="Frame">
        <Setter Property="BackgroundColor" Value="White"/>
        <Setter Property="BorderColor"     Value="{StaticResource Border}"/>
        <Setter Property="CornerRadius"    Value="8"/>
        <Setter Property="HasShadow"       Value="False"/>
        <Setter Property="Padding"         Value="13,9"/>
      </Style>

      <!-- ══════════════════════════════════════════
           ADMIN BADGE (.admin-badge)
      ══════════════════════════════════════════ -->
      <Style x:Key="AdminBadgeFrame" TargetType="Frame">
        <Setter Property="BackgroundColor" Value="#081A12"/>
        <Setter Property="BorderColor"     Value="#144A30"/>
        <Setter Property="CornerRadius"    Value="8"/>
        <Setter Property="HasShadow"       Value="False"/>
        <Setter Property="Padding"         Value="12,10"/>
      </Style>

      <!-- ══════════════════════════════════════════
           MODAL CARD (.modal)
      ══════════════════════════════════════════ -->
      <Style x:Key="ModalCard" TargetType="Frame">
        <Setter Property="BackgroundColor"  Value="White"/>
        <Setter Property="CornerRadius"     Value="20"/>
        <Setter Property="HasShadow"        Value="False"/>
        <Setter Property="BorderColor"      Value="{StaticResource Border}"/>
        <Setter Property="Padding"          Value="0"/>
        <Setter Property="WidthRequest"     Value="460"/>
      </Style>

    </ResourceDictionary>
  </Application.Resources>
</Application>
