<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="ProjectBReady.Views.inventoryadd"
             BackgroundColor="{StaticResource MainBg}">

  <Grid ColumnDefinitions="260,*">

    <!-- SIDEBAR -->
    <Grid Grid.Column="0" RowDefinitions="Auto,*,Auto"
          BackgroundColor="{StaticResource SidebarBg}">
      <VerticalStackLayout Grid.Row="0" Padding="24,28,24,20" Spacing="0">
        <Frame BackgroundColor="{StaticResource Accent}"
               CornerRadius="10" WidthRequest="40" HeightRequest="40"
               Padding="0" HasShadow="False" BorderColor="Transparent"
               HorizontalOptions="Start" Margin="0,0,0,12">
          <Label Text="🛡️" FontSize="20" HorizontalOptions="Center" VerticalOptions="Center"/>
        </Frame>
        <Label Text="b-ready" TextColor="White" FontSize="17" FontAttributes="Bold"/>
        <Label Text="BARANGAY DISASTER RELIEF"
               TextColor="{StaticResource TextMuted}"
               FontSize="10" CharacterSpacing="1" Margin="0,3,0,0"/>
        <BoxView HeightRequest="1" Color="#0F172A" Margin="0,20,0,0" Opacity="0.5"/>
      </VerticalStackLayout>
      <VerticalStackLayout Grid.Row="1" Padding="12,16" Spacing="0">
        <Label Text="MAIN" Style="{StaticResource NavSectionLabel}"/>
        <Button Text="📊  Dashboard"  Style="{StaticResource NavItem}"       Clicked="OnNavDashboard"/>
        <Button Text="🏛️  Shelters"   Style="{StaticResource NavItem}"       Clicked="OnNavShelters"/>
        <Button Text="📦  Inventory"  Style="{StaticResource NavItemActive}" Clicked="OnNavInventory"/>
        <Button Text="📋  Reports"    Style="{StaticResource NavItem}"       Clicked="OnNavReports"/>
      </VerticalStackLayout>
      <VerticalStackLayout Grid.Row="2" Padding="12,16">
        <BoxView HeightRequest="1" Color="#0F172A" Opacity="0.4" Margin="0,0,0,12"/>
        <Frame Style="{StaticResource AdminBadgeFrame}">
          <HorizontalStackLayout Spacing="8">
            <Ellipse Fill="{StaticResource Green}"
                     WidthRequest="8" HeightRequest="8" VerticalOptions="Center"/>
            <VerticalStackLayout VerticalOptions="Center">
              <Label Text="Admin Mode Active" Style="{StaticResource AdminText}"/>
              <Label Text="Ctrl+Shift+O to lock" Style="{StaticResource AdminHint}"/>
            </VerticalStackLayout>
          </HorizontalStackLayout>
        </Frame>
      </VerticalStackLayout>
    </Grid>

    <!-- MAIN -->
    <Grid Grid.Column="1" RowDefinitions="Auto,*">

      <!-- TOPBAR -->
      <Grid Grid.Row="0" Padding="32,20"
            BackgroundColor="{StaticResource CardBg}"
            ColumnDefinitions="*,Auto">
        <BoxView Grid.ColumnSpan="2" HeightRequest="1"
                 Color="{StaticResource Border}" VerticalOptions="End"/>
        <VerticalStackLayout Grid.Column="0" VerticalOptions="Center">
          <Label Text="Add Inventory Item" Style="{StaticResource PageH1}"/>
          <Label Text="Add a new item to the relief inventory" Style="{StaticResource PageSubtitle}"/>
        </VerticalStackLayout>
        <Button Grid.Column="1" Text="← Back to Inventory"
                Style="{StaticResource BtnGhost}"
                VerticalOptions="Center"
                Clicked="OnBackToInventory"/>
      </Grid>

      <!-- FORM BODY — centered card matching .form-card -->
      <ScrollView Grid.Row="1">
        <VerticalStackLayout Padding="32,36"
                             HorizontalOptions="Center">

          <Frame Style="{StaticResource FormCard}">
            <VerticalStackLayout>

              <!-- Form Card Header -->
              <VerticalStackLayout Padding="28,24,28,18">
                <HorizontalStackLayout Spacing="14">
                  <Frame x:Name="HeaderIconFrame"
                         BackgroundColor="{StaticResource AccentLight}"
                         CornerRadius="12" WidthRequest="44" HeightRequest="44"
                         Padding="0" HasShadow="False" BorderColor="Transparent">
                    <Label x:Name="HeaderIcon" Text="📦"
                           FontSize="22" HorizontalOptions="Center" VerticalOptions="Center"/>
                  </Frame>
                  <VerticalStackLayout VerticalOptions="Center">
                    <Label x:Name="HeaderTitle"
                           Text="Add Inventory Item"
                           FontSize="18" FontAttributes="Bold"
                           TextColor="{StaticResource TextPrimary}"/>
                    <Label Text="Select a type then fill in the details"
                           FontSize="13" TextColor="{StaticResource TextMuted}"
                           Margin="0,2,0,0"/>
                  </VerticalStackLayout>
                </HorizontalStackLayout>
              </VerticalStackLayout>
              <BoxView HeightRequest="1" Color="{StaticResource Border}"/>

              <!-- Form Body -->
              <VerticalStackLayout Padding="28">

                <!-- Type Toggle (.type-toggle) -->
                <Label Text="ITEM TYPE" Style="{StaticResource FormSectionTitle}"/>
                <Grid ColumnDefinitions="*,*" ColumnSpacing="10" Margin="0,0,0,20">
                  <Frame x:Name="BtnFoodFrame"
                         BackgroundColor="{StaticResource AccentLight}"
                         BorderColor="{StaticResource Accent}"
                         CornerRadius="12" Padding="0" HasShadow="False">
                    <Button x:Name="BtnFood"
                            Text="🍚  Food"
                            BackgroundColor="Transparent"
                            TextColor="{StaticResource Accent}"
                            FontSize="14" FontAttributes="Bold"
                            Padding="12" Clicked="OnSelectFood"/>
                  </Frame>
                  <Frame x:Name="BtnMedFrame"
                         Grid.Column="1"
                         BackgroundColor="White"
                         BorderColor="{StaticResource Border}"
                         CornerRadius="12" Padding="0" HasShadow="False">
                    <Button x:Name="BtnMed"
                            Text="💊  Medical Supply"
                            BackgroundColor="Transparent"
                            TextColor="{StaticResource TextSecondary}"
                            FontSize="14"
                            Padding="12" Clicked="OnSelectMedical"/>
                  </Frame>
                </Grid>

                <!-- Item Details -->
                <Label Text="ITEM DETAILS" Style="{StaticResource FormSectionTitle}"/>
                <BoxView HeightRequest="1" Color="{StaticResource Border}" Margin="0,0,0,14"/>

                <!-- Item Name -->
                <VerticalStackLayout Margin="0,0,0,16">
                  <Label Style="{StaticResource FormLabel}">
                    <Label.FormattedText>
                      <FormattedString>
                        <Span Text="Item Name "/>
                        <Span Text="*" TextColor="{StaticResource Red}"/>
                      </FormattedString>
                    </Label.FormattedText>
                  </Label>
                  <Frame Style="{StaticResource FormInputFrame}">
                    <Entry x:Name="ItemNameEntry"
                           Placeholder="e.g. Rice (25kg sack)"
                           PlaceholderColor="{StaticResource TextMuted}"
                           FontSize="14" BackgroundColor="Transparent"
                           TextColor="{StaticResource TextPrimary}"/>
                  </Frame>
                </VerticalStackLayout>

                <!-- Quantity -->
                <VerticalStackLayout Margin="0,0,0,16">
                  <Label Style="{StaticResource FormLabel}">
                    <Label.FormattedText>
                      <FormattedString>
                        <Span Text="Quantity "/>
                        <Span Text="*" TextColor="{StaticResource Red}"/>
                      </FormattedString>
                    </Label.FormattedText>
                  </Label>
                  <Frame Style="{StaticResource FormInputFrame}">
                    <Entry x:Name="ItemQtyEntry"
                           Keyboard="Numeric"
                           Placeholder="0"
                           PlaceholderColor="{StaticResource TextMuted}"
                           FontSize="14" BackgroundColor="Transparent"
                           TextColor="{StaticResource TextPrimary}"/>
                  </Frame>
                </VerticalStackLayout>

                <!-- Food-only: Expiration Date -->
                <VerticalStackLayout x:Name="FoodFields" Margin="0,0,0,16">
                  <Label Style="{StaticResource FormLabel}">
                    <Label.FormattedText>
                      <FormattedString>
                        <Span Text="Expiration Date "/>
                        <Span Text="*" TextColor="{StaticResource Red}"/>
                      </FormattedString>
                    </Label.FormattedText>
                  </Label>
                  <Frame Style="{StaticResource FormInputFrame}">
                    <DatePicker x:Name="ExpDatePicker"
                                Format="MMM dd, yyyy"
                                FontSize="14"
                                TextColor="{StaticResource TextPrimary}"
                                BackgroundColor="Transparent"/>
                  </Frame>
                </VerticalStackLayout>

                <!-- Medical-only fields -->
                <VerticalStackLayout x:Name="MedFields" IsVisible="False">
                  <VerticalStackLayout Margin="0,0,0,16">
                    <Label Text="Dosage" Style="{StaticResource FormLabel}"/>
                    <Frame Style="{StaticResource FormInputFrame}">
                      <Entry x:Name="DosageEntry"
                             Placeholder="e.g. 500mg / 3x daily"
                             PlaceholderColor="{StaticResource TextMuted}"
                             FontSize="14" BackgroundColor="Transparent"
                             TextColor="{StaticResource TextPrimary}"/>
                    </Frame>
                  </VerticalStackLayout>
                  <HorizontalStackLayout Spacing="10" Margin="0,0,0,16">
                    <CheckBox x:Name="RxCheckbox" Color="{StaticResource Accent}"/>
                    <Label Text="Prescription Required"
                           FontSize="14" TextColor="{StaticResource TextSecondary}"
                           VerticalOptions="Center"/>
                  </HorizontalStackLayout>
                </VerticalStackLayout>

                <!-- Error / Success messages -->
                <Frame x:Name="ErrorFrame" Style="{StaticResource AlertDanger}"
                       IsVisible="False" Margin="0,8,0,0">
                  <Label x:Name="ErrorLabel" FontSize="13" TextColor="#991B1B"/>
                </Frame>
                <Frame x:Name="SuccessFrame"
                       BackgroundColor="{StaticResource GreenLight}"
                       BorderColor="#6EE7B7"
                       CornerRadius="12" HasShadow="False"
                       Padding="18,13"
                       IsVisible="False" Margin="0,8,0,0">
                  <Label x:Name="SuccessLabel" FontSize="13"
                         TextColor="{StaticResource BadgeGreenText}"/>
                </Frame>

              </VerticalStackLayout>

              <!-- Form Card Footer -->
              <BoxView HeightRequest="1" Color="{StaticResource Border}"/>
              <HorizontalStackLayout Padding="28,18"
                                     HorizontalOptions="End"
                                     Spacing="10">
                <Button Text="✕ Cancel"
                        Style="{StaticResource BtnGhost}"
                        Clicked="OnCancel"/>
                <Button Text="💾 Save Item"
                        Style="{StaticResource BtnPrimary}"
                        Clicked="OnSaveItem"/>
              </HorizontalStackLayout>

            </VerticalStackLayout>
          </Frame>
        </VerticalStackLayout>
      </ScrollView>
    </Grid>
  </Grid>
</ContentPage>
