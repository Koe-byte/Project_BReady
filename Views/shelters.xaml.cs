<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="ProjectBReady.Views.shelter"
             BackgroundColor="{StaticResource MainBg}">

  <Grid ColumnDefinitions="260,*">

    <!-- ═══════════════════════════════════════
         SIDEBAR
    ═══════════════════════════════════════ -->
    <Grid Grid.Column="0"
          RowDefinitions="Auto,*,Auto"
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

    <!-- ═══════════════════════════════════════
         MAIN
    ═══════════════════════════════════════ -->
    <Grid Grid.Column="1" RowDefinitions="Auto,*">

      <!-- TOPBAR -->
      <Grid Grid.Row="0" Padding="32,20"
            BackgroundColor="{StaticResource CardBg}"
            ColumnDefinitions="*,Auto">
        <BoxView Grid.ColumnSpan="2" HeightRequest="1"
                 Color="{StaticResource Border}" VerticalOptions="End"/>
        <VerticalStackLayout Grid.Column="0" VerticalOptions="Center">
          <Label Text="Shelter Management" Style="{StaticResource PageH1}"/>
          <Label Text="Manage evacuation centers and occupancy records"
                 Style="{StaticResource PageSubtitle}"/>
        </VerticalStackLayout>
        <HorizontalStackLayout Grid.Column="1" VerticalOptions="Center" Spacing="10">
          <Button Text="⟳  Refresh"   Style="{StaticResource BtnGhost}"   Clicked="OnRefresh"/>
          <Button Text="+ Add Shelter" Style="{StaticResource BtnPrimary}" Clicked="OnAddShelter"/>
        </HorizontalStackLayout>
      </Grid>

      <!-- PAGE BODY -->
      <ScrollView Grid.Row="1">
        <VerticalStackLayout Padding="32,28" Spacing="0">

          <!-- Stat Cards (3-col) -->
          <Grid ColumnDefinitions="*,*,*" ColumnSpacing="16" Margin="0,0,0,28">
            <Frame Grid.Column="0" Style="{StaticResource StatCard}">
              <VerticalStackLayout>
                <Frame BackgroundColor="{StaticResource TealLight}"
                       CornerRadius="12" WidthRequest="44" HeightRequest="44"
                       Padding="0" HasShadow="False" BorderColor="Transparent"
                       HorizontalOptions="Start" Margin="0,0,0,14">
                  <Label Text="🏛️" FontSize="20" HorizontalOptions="Center" VerticalOptions="Center"/>
                </Frame>
                <Label Text="{Binding TotalShelters}"   Style="{StaticResource StatValue}"/>
                <Label Text="Total Shelters"            Style="{StaticResource StatLabel}"/>
                <Label Text="All registered centers"   Style="{StaticResource StatSub}"/>
                <BoxView HeightRequest="3" CornerRadius="2" Margin="0,12,-24,-22"
                         BackgroundColor="{StaticResource Teal}"/>
              </VerticalStackLayout>
            </Frame>
            <Frame Grid.Column="1" Style="{StaticResource StatCard}">
              <VerticalStackLayout>
                <Frame BackgroundColor="{StaticResource AccentLight}"
                       CornerRadius="12" WidthRequest="44" HeightRequest="44"
                       Padding="0" HasShadow="False" BorderColor="Transparent"
                       HorizontalOptions="Start" Margin="0,0,0,14">
                  <Label Text="👥" FontSize="20" HorizontalOptions="Center" VerticalOptions="Center"/>
                </Frame>
                <Label Text="{Binding TotalOccupancy}"      Style="{StaticResource StatValue}"/>
                <Label Text="Total Occupancy"               Style="{StaticResource StatLabel}"/>
                <Label Text="{Binding CombinedCapacityNote}" Style="{StaticResource StatSub}"/>
                <BoxView HeightRequest="3" CornerRadius="2" Margin="0,12,-24,-22"
                         BackgroundColor="{StaticResource Accent}"/>
              </VerticalStackLayout>
            </Frame>
            <Frame Grid.Column="2" Style="{StaticResource StatCard}">
              <VerticalStackLayout>
                <Frame BackgroundColor="{StaticResource RedLight}"
                       CornerRadius="12" WidthRequest="44" HeightRequest="44"
                       Padding="0" HasShadow="False" BorderColor="Transparent"
                       HorizontalOptions="Start" Margin="0,0,0,14">
                  <Label Text="🚫" FontSize="20" HorizontalOptions="Center" VerticalOptions="Center"/>
                </Frame>
                <Label Text="{Binding FullShelterCount}"   Style="{StaticResource StatValue}"/>
                <Label Text="Shelters at Full"             Style="{StaticResource StatLabel}"/>
                <Label Text="Require overflow action"      Style="{StaticResource StatSub}"/>
                <BoxView HeightRequest="3" CornerRadius="2" Margin="0,12,-24,-22"
                         BackgroundColor="{StaticResource Red}"/>
              </VerticalStackLayout>
            </Frame>
          </Grid>

          <!-- Table Card -->
          <Frame Style="{StaticResource Card}">
            <VerticalStackLayout>

              <!-- Toolbar (.toolbar) -->
              <Grid Padding="14,14" BackgroundColor="{StaticResource ToolbarBg}"
                    ColumnDefinitions="Auto,Auto,Auto,Auto,*,Auto">
                <BoxView Grid.ColumnSpan="6" HeightRequest="1"
                         Color="{StaticResource Border}" VerticalOptions="End"/>
                <!-- Search -->
                <Frame Grid.Column="0" Style="{StaticResource SearchBoxFrame}" WidthRequest="210">
                  <HorizontalStackLayout Spacing="8">
                    <Label Text="🔍" TextColor="{StaticResource TextMuted}" VerticalOptions="Center"/>
                    <Entry Placeholder="Search shelter name…"
                           PlaceholderColor="{StaticResource TextMuted}"
                           TextColor="{StaticResource TextPrimary}"
                           FontSize="13" BackgroundColor="Transparent"
                           TextChanged="OnSearchChanged"/>
                  </HorizontalStackLayout>
                </Frame>
                <BoxView Grid.Column="1" WidthRequest="1" HeightRequest="24"
                         Color="{StaticResource Border}" VerticalOptions="Center" Margin="8,0"/>
                <Button Grid.Column="2" Text="✏️ Edit"
                        Style="{StaticResource BtnGhostSm}"
                        Clicked="OnEditSelected"/>
                <Button Grid.Column="3" Text="🗑 Delete"
                        Style="{StaticResource BtnGhostSmDanger}"
                        Clicked="OnDeleteSelected"/>
                <Button Grid.Column="5" Text="📋 Update Occupancy"
                        Style="{StaticResource BtnTealSm}"
                        Clicked="OnUpdateOccupancy"/>
              </Grid>

              <!-- Table Header -->
              <Grid ColumnDefinitions="36,32,2*,*,*,*,2*,*,*"
                    Padding="16,12"
                    BackgroundColor="{StaticResource TableHeaderBg}">
                <!-- radio col -->
                <Label Grid.Column="1" Text="#"             Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="2" Text="SHELTER NAME"  Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="3" Text="MAX CAP."      Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="4" Text="CURRENT"       Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="5" Text="AVAILABLE"     Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="6" Text="OCCUPANCY"     Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="7" Text="STATUS"        Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="8" Text="ACTIONS"       Style="{StaticResource TableHeader}"/>
              </Grid>
              <BoxView HeightRequest="1" Color="{StaticResource Border}"/>

              <!-- Rows -->
              <CollectionView x:Name="ShelterTable"
                              ItemsSource="{Binding Shelters}"
                              SelectionMode="Single"
                              SelectionChanged="OnShelterSelected">
                <CollectionView.ItemTemplate>
                  <DataTemplate>
                    <Grid ColumnDefinitions="36,32,2*,*,*,*,2*,*,*"
                          Padding="16,0"
                          BackgroundColor="{Binding RowBg}">
                      <RadioButton Grid.Column="0" VerticalOptions="Center"/>
                      <Label Grid.Column="1" Text="{Binding RowNumber}"
                             Style="{StaticResource TableCellMuted}"/>
                      <Label Grid.Column="2" Text="{Binding Name}"
                             Style="{StaticResource TableCellBold}"/>
                      <Label Grid.Column="3" Text="{Binding MaxCapacity}"
                             Style="{StaticResource TableCell}"/>
                      <Label Grid.Column="4" Text="{Binding CurrentOccupancy}"
                             Style="{StaticResource TableCell}"/>
                      <!-- Available — teal if > 0, red if 0 -->
                      <Label Grid.Column="5" Text="{Binding Available}"
                             Style="{StaticResource TableCell}"
                             TextColor="{Binding AvailableColor}"/>
                      <!-- Progress bar -->
                      <Grid Grid.Column="6" ColumnDefinitions="*,60" VerticalOptions="Center">
                        <Grid Grid.Column="0" HeightRequest="6" Margin="0,0,10,0">
                          <BoxView BackgroundColor="{StaticResource Border}"
                                   CornerRadius="3" HeightRequest="6"/>
                          <BoxView BackgroundColor="{Binding FillColor}"
                                   CornerRadius="3" HeightRequest="6"
                                   HorizontalOptions="Start"
                                   WidthRequest="{Binding BarWidth}"/>
                        </Grid>
                        <Label Grid.Column="1" Text="{Binding PctFull}"
                               FontSize="12" TextColor="{StaticResource TextMuted}"
                               VerticalOptions="Center"/>
                      </Grid>
                      <!-- Status badge -->
                      <Frame Grid.Column="7"
                             BackgroundColor="{Binding StatusBadgeBg}"
                             BorderColor="Transparent" CornerRadius="20"
                             Padding="10,3" HasShadow="False"
                             HorizontalOptions="Start" VerticalOptions="Center">
                        <Label Text="{Binding Status}"
                               FontSize="11" FontAttributes="Bold"
                               TextColor="{Binding StatusTextColor}"/>
                      </Frame>
                      <!-- Edit button -->
                      <Button Grid.Column="8" Text="✏️ Edit"
                              Style="{StaticResource BtnGhostSm}"
                              Clicked="OnEditRow"
                              CommandParameter="{Binding .}"
                              VerticalOptions="Center"/>
                      <BoxView Grid.ColumnSpan="9" HeightRequest="1"
                               Color="{StaticResource Border}" VerticalOptions="End"/>
                    </Grid>
                  </DataTemplate>
                </CollectionView.ItemTemplate>
              </CollectionView>
            </VerticalStackLayout>
          </Frame>

        </VerticalStackLayout>
      </ScrollView>
    </Grid>

    <!-- ═══════════════════════════════════════
         MODALS
    ═══════════════════════════════════════ -->

    <!-- Update Occupancy Modal -->
    <Grid x:Name="ModalOccupancyOverlay"
          Grid.ColumnSpan="2"
          IsVisible="False"
          BackgroundColor="#73000000">
      <Frame Style="{StaticResource ModalCard}"
             HorizontalOptions="Center" VerticalOptions="Center">
        <VerticalStackLayout>
          <Grid Padding="26,22,26,16" ColumnDefinitions="*,Auto">
            <BoxView Grid.ColumnSpan="2" HeightRequest="1"
                     Color="{StaticResource Border}" VerticalOptions="End"/>
            <VerticalStackLayout Grid.Column="0">
              <Label Text="Update Occupancy" Style="{StaticResource ModalTitle}"/>
              <Label x:Name="OccShelterName" Style="{StaticResource ModalSub}"/>
            </VerticalStackLayout>
            <Button Grid.Column="1" Text="✕"
                    BackgroundColor="Transparent"
                    TextColor="{StaticResource TextMuted}"
                    FontSize="18" WidthRequest="32" HeightRequest="32"
                    Padding="0" CornerRadius="6"
                    Clicked="OnCloseOccModal"/>
          </Grid>
          <VerticalStackLayout Padding="26,24" Spacing="8">
            <Label Text="New Occupancy Count" Style="{StaticResource FormLabel}"/>
            <Frame Style="{StaticResource FormInputFrame}">
              <Entry x:Name="OccInput"
                     Keyboard="Numeric" Placeholder="0"
                     FontSize="14" BackgroundColor="Transparent"
                     TextColor="{StaticResource TextPrimary}"/>
            </Frame>
            <Label Text="Status will automatically update to Full when occupancy reaches max capacity."
                   FontSize="12" TextColor="{StaticResource TextMuted}"/>
          </VerticalStackLayout>
          <Grid Padding="26,16" ColumnDefinitions="*,Auto,Auto">
            <BoxView Grid.ColumnSpan="3" HeightRequest="1"
                     Color="{StaticResource Border}" VerticalOptions="Start"/>
            <Button Grid.Column="1" Text="Cancel"
                    Style="{StaticResource BtnGhost}"
                    Clicked="OnCloseOccModal"/>
            <Button Grid.Column="2" Text="💾 Save Occupancy"
                    Style="{StaticResource BtnTeal}"
                    Margin="10,0,0,0"
                    Clicked="OnSaveOccupancy"/>
          </Grid>
        </VerticalStackLayout>
      </Frame>
    </Grid>

    <!-- Delete Shelter Modal -->
    <Grid x:Name="ModalDeleteOverlay"
          Grid.ColumnSpan="2"
          IsVisible="False"
          BackgroundColor="#73000000">
      <Frame Style="{StaticResource ModalCard}"
             HorizontalOptions="Center" VerticalOptions="Center">
        <VerticalStackLayout>
          <Grid Padding="26,22,26,16" ColumnDefinitions="*,Auto">
            <BoxView Grid.ColumnSpan="2" HeightRequest="1"
                     Color="{StaticResource Border}" VerticalOptions="End"/>
            <VerticalStackLayout Grid.Column="0">
              <Label Text="Delete Shelter" Style="{StaticResource ModalTitle}"/>
              <Label Text="This action cannot be undone" Style="{StaticResource ModalSub}"/>
            </VerticalStackLayout>
            <Button Grid.Column="1" Text="✕"
                    BackgroundColor="Transparent"
                    TextColor="{StaticResource TextMuted}"
                    FontSize="18" WidthRequest="32" HeightRequest="32"
                    Padding="0" CornerRadius="6"
                    Clicked="OnCloseDeleteModal"/>
          </Grid>
          <VerticalStackLayout Padding="26,24">
            <Frame Style="{StaticResource AlertDanger}">
              <Label x:Name="DeleteShelterLabel"
                     FontSize="13" TextColor="#991B1B"/>
            </Frame>
          </VerticalStackLayout>
          <Grid Padding="26,16" ColumnDefinitions="*,Auto,Auto">
            <BoxView Grid.ColumnSpan="3" HeightRequest="1"
                     Color="{StaticResource Border}" VerticalOptions="Start"/>
            <Button Grid.Column="1" Text="Cancel"
                    Style="{StaticResource BtnGhost}"
                    Clicked="OnCloseDeleteModal"/>
            <Button Grid.Column="2" Text="🗑 Yes, Delete"
                    Style="{StaticResource BtnDanger}"
                    Margin="10,0,0,0"
                    Clicked="OnConfirmDelete"/>
          </Grid>
        </VerticalStackLayout>
      </Frame>
    </Grid>

  </Grid>
</ContentPage>
