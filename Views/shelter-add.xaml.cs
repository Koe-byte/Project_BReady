<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="ProjectBReady.Views.shelteradd"
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
        <Button Text="🏛️  Shelters"   Style="{StaticResource NavItemActive}" Clicked="OnNavShelters"/>
        <Button Text="📦  Inventory"  Style="{StaticResource NavItem}"       Clicked="OnNavInventory"/>
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
          <Label Text="Add New Shelter" Style="{StaticResource PageH1}"/>
          <Label Text="Register a new evacuation center" Style="{StaticResource PageSubtitle}"/>
        </VerticalStackLayout>
        <Button Grid.Column="1" Text="← Back to Shelters"
                Style="{StaticResource BtnGhost}"
                VerticalOptions="Center"
                Clicked="OnBackToShelters"/>
      </Grid>

      <!-- FORM BODY -->
      <ScrollView Grid.Row="1">
        <VerticalStackLayout Padding="32,36" HorizontalOptions="Center">

          <Frame Style="{StaticResource FormCard}">
            <VerticalStackLayout>

              <!-- Card Header -->
              <VerticalStackLayout Padding="28,24,28,18">
                <HorizontalStackLayout Spacing="14">
                  <Frame BackgroundColor="{StaticResource AccentLight}"
                         CornerRadius="12" WidthRequest="44" HeightRequest="44"
                         Padding="0" HasShadow="False" BorderColor="Transparent">
                    <Label Text="🏛️" FontSize="22"
                           HorizontalOptions="Center" VerticalOptions="Center"/>
                  </Frame>
                  <VerticalStackLayout VerticalOptions="Center">
                    <Label Text="Shelter Details"
                           FontSize="18" FontAttributes="Bold"
                           TextColor="{StaticResource TextPrimary}"/>
                    <Label Text="Fill in all required fields below"
                           FontSize="13" TextColor="{StaticResource TextMuted}"
                           Margin="0,2,0,0"/>
                  </VerticalStackLayout>
                </HorizontalStackLayout>
              </VerticalStackLayout>
              <BoxView HeightRequest="1" Color="{StaticResource Border}"/>

              <!-- Card Body -->
              <VerticalStackLayout Padding="28">

                <Label Text="BASIC INFORMATION" Style="{StaticResource FormSectionTitle}"/>
                <BoxView HeightRequest="1" Color="{StaticResource Border}" Margin="0,0,0,14"/>

                <!-- Shelter Name -->
                <VerticalStackLayout Margin="0,0,0,16">
                  <Label Style="{StaticResource FormLabel}">
                    <Label.FormattedText>
                      <FormattedString>
                        <Span Text="Shelter Name "/>
                        <Span Text="*" TextColor="{StaticResource Red}"/>
                      </FormattedString>
                    </Label.FormattedText>
                  </Label>
                  <Frame Style="{StaticResource FormInputFrame}">
                    <Entry x:Name="ShelterNameEntry"
                           Placeholder="e.g. Barangay Hall Gymnasium"
                           PlaceholderColor="{StaticResource TextMuted}"
                           FontSize="14" BackgroundColor="Transparent"
                           TextColor="{StaticResource TextPrimary}"/>
                  </Frame>
                </VerticalStackLayout>

                <!-- Capacity Row -->
                <Grid ColumnDefinitions="*,*" ColumnSpacing="16" Margin="0,0,0,16">
                  <VerticalStackLayout Grid.Column="0">
                    <Label Style="{StaticResource FormLabel}">
                      <Label.FormattedText>
                        <FormattedString>
                          <Span Text="Maximum Capacity "/>
                          <Span Text="*" TextColor="{StaticResource Red}"/>
                        </FormattedString>
                      </Label.FormattedText>
                    </Label>
                    <Frame Style="{StaticResource FormInputFrame}">
                      <Entry x:Name="MaxCapEntry"
                             Keyboard="Numeric" Placeholder="200"
                             PlaceholderColor="{StaticResource TextMuted}"
                             FontSize="14" BackgroundColor="Transparent"
                             TextColor="{StaticResource TextPrimary}"/>
                    </Frame>
                  </VerticalStackLayout>
                  <VerticalStackLayout Grid.Column="1">
                    <Label Text="Current Occupancy" Style="{StaticResource FormLabel}"/>
                    <Frame Style="{StaticResource FormInputFrame}">
                      <Entry x:Name="CurrOccEntry"
                             Keyboard="Numeric" Placeholder="0"
                             PlaceholderColor="{StaticResource TextMuted}"
                             FontSize="14" BackgroundColor="Transparent"
                             TextColor="{StaticResource TextPrimary}"/>
                    </Frame>
                  </VerticalStackLayout>
                </Grid>

                <!-- Status -->
                <VerticalStackLayout Margin="0,0,0,16">
                  <Label Text="Status" Style="{StaticResource FormLabel}"/>
                  <Frame Style="{StaticResource FormInputFrame}">
                    <Picker x:Name="StatusPicker"
                            FontSize="14"
                            TextColor="{StaticResource TextPrimary}"
                            BackgroundColor="Transparent">
                      <Picker.Items>
                        <x:String>Open</x:String>
                        <x:String>Full</x:String>
                        <x:String>Closed</x:String>
                        <x:String>Under Maintenance</x:String>
                      </Picker.Items>
                    </Picker>
                  </Frame>
                  <Label Text="Status will auto-update to Full when occupancy reaches max capacity on save."
                         FontSize="12" TextColor="{StaticResource TextMuted}" Margin="0,6,0,0"/>
                </VerticalStackLayout>

                <!-- Error / Success -->
                <Frame x:Name="ErrorFrame" Style="{StaticResource AlertDanger}"
                       IsVisible="False" Margin="0,4,0,0">
                  <Label x:Name="ErrorLabel" FontSize="13" TextColor="#991B1B"/>
                </Frame>
                <Frame x:Name="SuccessFrame"
                       BackgroundColor="{StaticResource GreenLight}"
                       BorderColor="#6EE7B7"
                       CornerRadius="12" HasShadow="False"
                       Padding="18,13"
                       IsVisible="False" Margin="0,4,0,0">
                  <Label x:Name="SuccessLabel" FontSize="13"
                         TextColor="{StaticResource BadgeGreenText}"/>
                </Frame>

              </VerticalStackLayout>

              <!-- Card Footer -->
              <BoxView HeightRequest="1" Color="{StaticResource Border}"/>
              <HorizontalStackLayout Padding="28,18"
                                     HorizontalOptions="End"
                                     Spacing="10">
                <Button Text="✕ Cancel"
                        Style="{StaticResource BtnGhost}"
                        Clicked="OnCancel"/>
                <Button Text="💾 Save Shelter"
                        Style="{StaticResource BtnPrimary}"
                        Clicked="OnSaveShelter"/>
              </HorizontalStackLayout>

            </VerticalStackLayout>
          </Frame>
        </VerticalStackLayout>
      </ScrollView>
    </Grid>
  </Grid>
</ContentPage>
