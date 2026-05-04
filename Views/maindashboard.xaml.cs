<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="ProjectBReady.maindashboard"
             BackgroundColor="{StaticResource MainBg}">

  <Grid ColumnDefinitions="260,*">

    <!-- ═══════════════════════════════════════
         SIDEBAR  — matches sidebar.js HTML
    ═══════════════════════════════════════ -->
    <Grid Grid.Column="0"
          RowDefinitions="Auto,*,Auto"
          BackgroundColor="{StaticResource SidebarBg}">

      <!-- Brand block (.sidebar-brand) -->
      <VerticalStackLayout Grid.Row="0"
                           Padding="24,28,24,20"
                           Spacing="0">
        <!-- .brand-icon  orange gradient box -->
        <Frame BackgroundColor="{StaticResource Accent}"
               CornerRadius="10"
               WidthRequest="40" HeightRequest="40"
               Padding="0" HasShadow="False"
               BorderColor="Transparent"
               HorizontalOptions="Start"
               Margin="0,0,0,12">
          <Label Text="🛡️" FontSize="20"
                 HorizontalOptions="Center"
                 VerticalOptions="Center"/>
        </Frame>
        <!-- .brand-name -->
        <Label Text="b-ready"
               TextColor="White"
               FontSize="17"
               FontAttributes="Bold"/>
        <!-- .brand-sub -->
        <Label Text="BARANGAY DISASTER RELIEF"
               TextColor="{StaticResource TextMuted}"
               FontSize="10"
               CharacterSpacing="1"
               Margin="0,3,0,0"/>
        <!-- bottom border -->
        <BoxView HeightRequest="1"
                 Color="#0F172A"
                 Margin="0,20,0,0"
                 Opacity="0.5"/>
      </VerticalStackLayout>

      <!-- Nav (.sidebar-nav) -->
      <VerticalStackLayout Grid.Row="1"
                           Padding="12,16"
                           Spacing="0">
        <Label Text="MAIN" Style="{StaticResource NavSectionLabel}"/>
        <!-- Active page gets NavItemActive style -->
        <Button Text="📊  Dashboard"
                Style="{StaticResource NavItemActive}"
                Clicked="OnNavDashboard"/>
        <Button Text="🏛️  Shelters"
                Style="{StaticResource NavItem}"
                Clicked="OnNavShelters"/>
        <Button Text="📦  Inventory"
                Style="{StaticResource NavItem}"
                Clicked="OnNavInventory"/>
        <Button Text="📋  Reports"
                Style="{StaticResource NavItem}"
                Clicked="OnNavReports"/>
      </VerticalStackLayout>

      <!-- Admin footer (.sidebar-footer) -->
      <VerticalStackLayout Grid.Row="2"
                           Padding="12,16">
        <BoxView HeightRequest="1"
                 Color="#0F172A"
                 Opacity="0.4"
                 Margin="0,0,0,12"/>
        <Frame Style="{StaticResource AdminBadgeFrame}">
          <HorizontalStackLayout Spacing="8">
            <Ellipse Fill="{StaticResource Green}"
                     WidthRequest="8" HeightRequest="8"
                     VerticalOptions="Center"/>
            <VerticalStackLayout VerticalOptions="Center">
              <Label Text="Admin Mode Active"
                     Style="{StaticResource AdminText}"/>
              <Label Text="Ctrl+Shift+O to lock"
                     Style="{StaticResource AdminHint}"/>
            </VerticalStackLayout>
          </HorizontalStackLayout>
        </Frame>
      </VerticalStackLayout>
    </Grid>

    <!-- ═══════════════════════════════════════
         MAIN  — matches .main
    ═══════════════════════════════════════ -->
    <Grid Grid.Column="1" RowDefinitions="Auto,*">

      <!-- TOPBAR (.topbar) -->
      <Grid Grid.Row="0"
            Padding="32,20"
            BackgroundColor="{StaticResource CardBg}"
            ColumnDefinitions="*,Auto">
        <BoxView Grid.ColumnSpan="2"
                 HeightRequest="1"
                 Color="{StaticResource Border}"
                 VerticalOptions="End"/>
        <VerticalStackLayout Grid.Column="0" VerticalOptions="Center">
          <Label Text="Relief Operations Dashboard"
                 Style="{StaticResource PageH1}"/>
          <Label Text="Real-time overview of evacuation shelters and relief goods"
                 Style="{StaticResource PageSubtitle}"/>
        </VerticalStackLayout>
        <HorizontalStackLayout Grid.Column="1"
                               VerticalOptions="Center"
                               Spacing="10">
          <Button Text="⟳  Refresh"
                  Style="{StaticResource BtnGhost}"
                  Clicked="OnRefresh"/>
          <Button Text="+ Add Shelter"
                  Style="{StaticResource BtnPrimary}"
                  Clicked="OnAddShelter"/>
        </HorizontalStackLayout>
      </Grid>

      <!-- PAGE BODY (.page-body) -->
      <ScrollView Grid.Row="1">
        <VerticalStackLayout Padding="32,28" Spacing="0">

          <!-- Alert (.alert-warn) -->
          <Frame Style="{StaticResource AlertWarn}" Margin="0,0,0,20">
            <HorizontalStackLayout Spacing="12">
              <Label Text="⚠️" VerticalOptions="Center" FontSize="14"/>
              <Label VerticalOptions="Center" FontSize="13" TextColor="#92400E">
                <Label.FormattedText>
                  <FormattedString>
                    <Span Text="3 food items" FontAttributes="Bold"/>
                    <Span Text=" are expiring within the next 7 days. Review Inventory →"/>
                  </FormattedString>
                </Label.FormattedText>
              </Label>
            </HorizontalStackLayout>
          </Frame>

          <!-- Stat Cards (.stat-grid — 4 col) -->
          <Grid ColumnDefinitions="*,*,*,*"
                ColumnSpacing="16"
                Margin="0,0,0,28">
            <!-- Orange: evacuees -->
            <Frame Grid.Column="0" Style="{StaticResource StatCard}">
              <VerticalStackLayout>
                <Frame BackgroundColor="{StaticResource AccentLight}"
                       CornerRadius="12" WidthRequest="44" HeightRequest="44"
                       Padding="0" HasShadow="False" BorderColor="Transparent"
                       HorizontalOptions="Start" Margin="0,0,0,14">
                  <Label Text="👥" FontSize="20"
                         HorizontalOptions="Center" VerticalOptions="Center"/>
                </Frame>
                <Label Text="545" Style="{StaticResource StatValue}"/>
                <Label Text="Total Evacuees" Style="{StaticResource StatLabel}"/>
                <Label Text="of 780 total capacity" Style="{StaticResource StatSub}"/>
                <!-- bottom accent bar -->
                <BoxView HeightRequest="3" CornerRadius="2" Margin="0,12,-24,-22"
                         BackgroundColor="{StaticResource Accent}"/>
              </VerticalStackLayout>
            </Frame>
            <!-- Teal: shelters -->
            <Frame Grid.Column="1" Style="{StaticResource StatCard}">
              <VerticalStackLayout>
                <Frame BackgroundColor="{StaticResource TealLight}"
                       CornerRadius="12" WidthRequest="44" HeightRequest="44"
                       Padding="0" HasShadow="False" BorderColor="Transparent"
                       HorizontalOptions="Start" Margin="0,0,0,14">
                  <Label Text="🏛️" FontSize="20"
                         HorizontalOptions="Center" VerticalOptions="Center"/>
                </Frame>
                <Label Text="3" Style="{StaticResource StatValue}"/>
                <Label Text="Open Shelters" Style="{StaticResource StatLabel}"/>
                <Label Text="5 shelters total" Style="{StaticResource StatSub}"/>
                <BoxView HeightRequest="3" CornerRadius="2" Margin="0,12,-24,-22"
                         BackgroundColor="{StaticResource Teal}"/>
              </VerticalStackLayout>
            </Frame>
            <!-- Blue: items -->
            <Frame Grid.Column="2" Style="{StaticResource StatCard}">
              <VerticalStackLayout>
                <Frame BackgroundColor="{StaticResource BlueLight}"
                       CornerRadius="12" WidthRequest="44" HeightRequest="44"
                       Padding="0" HasShadow="False" BorderColor="Transparent"
                       HorizontalOptions="Start" Margin="0,0,0,14">
                  <Label Text="📦" FontSize="20"
                         HorizontalOptions="Center" VerticalOptions="Center"/>
                </Frame>
                <Label Text="2,528" Style="{StaticResource StatValue}"/>
                <Label Text="Relief Items" Style="{StaticResource StatLabel}"/>
                <Label Text="8 item types tracked" Style="{StaticResource StatSub}"/>
                <BoxView HeightRequest="3" CornerRadius="2" Margin="0,12,-24,-22"
                         BackgroundColor="{StaticResource Blue}"/>
              </VerticalStackLayout>
            </Frame>
            <!-- Red: dispatches -->
            <Frame Grid.Column="3" Style="{StaticResource StatCard}">
              <VerticalStackLayout>
                <Frame BackgroundColor="{StaticResource RedLight}"
                       CornerRadius="12" WidthRequest="44" HeightRequest="44"
                       Padding="0" HasShadow="False" BorderColor="Transparent"
                       HorizontalOptions="Start" Margin="0,0,0,14">
                  <Label Text="🚚" FontSize="20"
                         HorizontalOptions="Center" VerticalOptions="Center"/>
                </Frame>
                <Label Text="5" Style="{StaticResource StatValue}"/>
                <Label Text="Dispatches" Style="{StaticResource StatLabel}"/>
                <Label Text="recent records" Style="{StaticResource StatSub}"/>
                <BoxView HeightRequest="3" CornerRadius="2" Margin="0,12,-24,-22"
                         BackgroundColor="{StaticResource Red}"/>
              </VerticalStackLayout>
            </Frame>
          </Grid>

          <!-- Two-col: Bar chart card + Donut card (.two-col) -->
          <Grid ColumnDefinitions="*,340" ColumnSpacing="20" Margin="0,0,0,20">

            <!-- Bar chart card -->
            <Frame Grid.Column="0" Style="{StaticResource Card}">
              <VerticalStackLayout>
                <Grid Padding="18,18,18,0" ColumnDefinitions="*,Auto">
                  <VerticalStackLayout Grid.Column="0">
                    <Label Text="Shelter Occupancy Overview" Style="{StaticResource CardTitle}"/>
                    <Label Text="Current vs. maximum capacity per shelter" Style="{StaticResource CardSubtitle}"/>
                  </VerticalStackLayout>
                </Grid>
                <BoxView HeightRequest="1" Color="{StaticResource Border}" Margin="0,14,0,0"/>
                <!-- CollectionView renders occupancy bars per shelter -->
                <CollectionView ItemsSource="{Binding Shelters}" Margin="18,16">
                  <CollectionView.ItemTemplate>
                    <DataTemplate>
                      <VerticalStackLayout Spacing="6" Margin="0,0,0,12">
                        <Grid ColumnDefinitions="*,Auto">
                          <Label Text="{Binding Name}"
                                 FontSize="13" FontAttributes="Bold"
                                 TextColor="{StaticResource TextPrimary}"/>
                          <Label Grid.Column="1"
                                 Text="{Binding OccupancyDisplay}"
                                 FontSize="12"
                                 TextColor="{StaticResource TextMuted}"/>
                        </Grid>
                        <!-- Progress bar -->
                        <Grid HeightRequest="6" ColumnDefinitions="*">
                          <BoxView BackgroundColor="{StaticResource Border}"
                                   CornerRadius="3" HeightRequest="6"/>
                          <BoxView BackgroundColor="{Binding FillColor}"
                                   CornerRadius="3" HeightRequest="6"
                                   HorizontalOptions="Start"
                                   WidthRequest="{Binding BarWidth}"/>
                        </Grid>
                      </VerticalStackLayout>
                    </DataTemplate>
                  </CollectionView.ItemTemplate>
                </CollectionView>
              </VerticalStackLayout>
            </Frame>

            <!-- Donut / status card -->
            <Frame Grid.Column="1" Style="{StaticResource Card}">
              <VerticalStackLayout>
                <VerticalStackLayout Padding="18,18,18,0">
                  <Label Text="Shelter Status" Style="{StaticResource CardTitle}"/>
                  <Label Text="Distribution by current status" Style="{StaticResource CardSubtitle}"/>
                </VerticalStackLayout>
                <BoxView HeightRequest="1" Color="{StaticResource Border}" Margin="0,14,0,0"/>
                <VerticalStackLayout Padding="18,16" Spacing="12">
                  <!-- Legend items -->
                  <Grid ColumnDefinitions="10,*,Auto">
                    <Ellipse Grid.Column="0" Fill="{StaticResource Teal}"
                             WidthRequest="10" HeightRequest="10" VerticalOptions="Center"/>
                    <Label Grid.Column="1" Text="Open" FontSize="13"
                           TextColor="{StaticResource TextPrimary}" Margin="10,0,0,0"/>
                    <Label Grid.Column="2" Text="3 (60%)" FontSize="13"
                           FontAttributes="Bold" TextColor="{StaticResource TextPrimary}"/>
                  </Grid>
                  <Grid ColumnDefinitions="10,*,Auto">
                    <Ellipse Grid.Column="0" Fill="{StaticResource Accent}"
                             WidthRequest="10" HeightRequest="10" VerticalOptions="Center"/>
                    <Label Grid.Column="1" Text="Full" FontSize="13"
                           TextColor="{StaticResource TextPrimary}" Margin="10,0,0,0"/>
                    <Label Grid.Column="2" Text="2 (40%)" FontSize="13"
                           FontAttributes="Bold" TextColor="{StaticResource TextPrimary}"/>
                  </Grid>
                  <Grid ColumnDefinitions="10,*,Auto">
                    <Ellipse Grid.Column="0" Fill="{StaticResource BorderStrong}"
                             WidthRequest="10" HeightRequest="10" VerticalOptions="Center"/>
                    <Label Grid.Column="1" Text="Closed / Maintenance" FontSize="13"
                           TextColor="{StaticResource TextPrimary}" Margin="10,0,0,0"/>
                    <Label Grid.Column="2" Text="0" FontSize="13"
                           FontAttributes="Bold" TextColor="{StaticResource TextPrimary}"/>
                  </Grid>
                </VerticalStackLayout>
              </VerticalStackLayout>
            </Frame>
          </Grid>

          <!-- Shelter Table Card -->
          <Frame Style="{StaticResource Card}">
            <VerticalStackLayout>
              <!-- card-header -->
              <Grid Padding="18,18,18,0" ColumnDefinitions="*,Auto">
                <VerticalStackLayout Grid.Column="0">
                  <Label Text="Evacuation Shelters" Style="{StaticResource CardTitle}"/>
                  <Label Text="Live occupancy status" Style="{StaticResource CardSubtitle}"/>
                </VerticalStackLayout>
                <Button Grid.Column="1" Text="View All →"
                        Style="{StaticResource BtnGhost}"
                        VerticalOptions="Center"
                        Clicked="OnViewAllShelters"/>
              </Grid>
              <BoxView HeightRequest="1" Color="{StaticResource Border}" Margin="0,14,0,0"/>
              <!-- Table header -->
              <Grid ColumnDefinitions="2*,2*,*,*,*"
                    Padding="16,12"
                    BackgroundColor="{StaticResource TableHeaderBg}">
                <Label Text="SHELTER NAME"  Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="1" Text="OCCUPANCY"   Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="2" Text="CAPACITY"    Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="3" Text="% FULL"      Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="4" Text="STATUS"      Style="{StaticResource TableHeader}"/>
              </Grid>
              <BoxView HeightRequest="1" Color="{StaticResource Border}"/>
              <!-- Rows -->
              <CollectionView ItemsSource="{Binding Shelters}">
                <CollectionView.ItemTemplate>
                  <DataTemplate>
                    <Grid ColumnDefinitions="2*,2*,*,*,*" Padding="16,0">
                      <Label Text="{Binding Name}" Style="{StaticResource TableCellBold}"/>
                      <Label Grid.Column="1" Text="{Binding OccupancyDisplay}"
                             Style="{StaticResource TableCell}"/>
                      <Label Grid.Column="2" Text="{Binding MaxCapacity}"
                             Style="{StaticResource TableCellMuted}"/>
                      <Label Grid.Column="3" Text="{Binding PctFull}"
                             Style="{StaticResource TableCell}"
                             TextColor="{Binding PctColor}"/>
                      <Frame Grid.Column="4"
                             BackgroundColor="{Binding StatusBadgeBg}"
                             BorderColor="Transparent"
                             CornerRadius="20" Padding="10,3"
                             HasShadow="False" HorizontalOptions="Start"
                             VerticalOptions="Center">
                        <Label Text="{Binding Status}"
                               FontSize="11" FontAttributes="Bold"
                               TextColor="{Binding StatusTextColor}"/>
                      </Frame>
                      <!-- Row separator -->
                      <BoxView Grid.ColumnSpan="5" HeightRequest="1"
                               Color="{StaticResource Border}"
                               VerticalOptions="End"/>
                    </Grid>
                  </DataTemplate>
                </CollectionView.ItemTemplate>
              </CollectionView>
            </VerticalStackLayout>
          </Frame>

          <!-- Dispatch Logs Card -->
          <Frame Style="{StaticResource Card}">
            <VerticalStackLayout>
              <VerticalStackLayout Padding="18,18,18,0">
                <Label Text="Recent Dispatch Logs" Style="{StaticResource CardTitle}"/>
                <Label Text="Latest relief goods distributed" Style="{StaticResource CardSubtitle}"/>
              </VerticalStackLayout>
              <BoxView HeightRequest="1" Color="{StaticResource Border}" Margin="0,14,0,0"/>
              <Grid ColumnDefinitions="2*,2*,*,2*"
                    Padding="16,12"
                    BackgroundColor="{StaticResource TableHeaderBg}">
                <Label Text="ITEM"               Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="1" Text="SHELTER DESTINATION" Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="2" Text="QTY"    Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="3" Text="DATE &amp; TIME" Style="{StaticResource TableHeader}"/>
              </Grid>
              <BoxView HeightRequest="1" Color="{StaticResource Border}"/>
              <CollectionView ItemsSource="{Binding RecentDispatches}">
                <CollectionView.ItemTemplate>
                  <DataTemplate>
                    <Grid ColumnDefinitions="2*,2*,*,2*" Padding="16,0">
                      <Label Text="{Binding ItemName}" Style="{StaticResource TableCellBold}"/>
                      <Label Grid.Column="1" Text="{Binding Destination}"
                             Style="{StaticResource TableCellMuted}"/>
                      <Label Grid.Column="2" Text="{Binding Qty}"
                             Style="{StaticResource TableCellBold}"/>
                      <Label Grid.Column="3" Text="{Binding DateTimeDisplay}"
                             Style="{StaticResource TableCellMuted}"/>
                      <BoxView Grid.ColumnSpan="4" HeightRequest="1"
                               Color="{StaticResource Border}"
                               VerticalOptions="End"/>
                    </Grid>
                  </DataTemplate>
                </CollectionView.ItemTemplate>
              </CollectionView>
            </VerticalStackLayout>
          </Frame>

        </VerticalStackLayout>
      </ScrollView>
    </Grid>
  </Grid>
</ContentPage>
