<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="ProjectBReady.Views.reports"
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
        <Button Text="🏛️  Shelters"   Style="{StaticResource NavItem}"       Clicked="OnNavShelters"/>
        <Button Text="📦  Inventory"  Style="{StaticResource NavItem}"       Clicked="OnNavInventory"/>
        <Button Text="📋  Reports"    Style="{StaticResource NavItemActive}" Clicked="OnNavReports"/>
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
          <Label Text="Reports &amp; Analytics" Style="{StaticResource PageH1}"/>
          <Label Text="Comprehensive summary of all relief operations"
                 Style="{StaticResource PageSubtitle}"/>
        </VerticalStackLayout>
        <HorizontalStackLayout Grid.Column="1" VerticalOptions="Center" Spacing="10">
          <Button Text="⟳  Refresh"     Style="{StaticResource BtnGhost}"   Clicked="OnRefresh"/>
          <Button Text="🖨 Print"        Style="{StaticResource BtnGhost}"   Clicked="OnPrint"/>
          <Button Text="⬇ Export CSV"   Style="{StaticResource BtnPrimary}" Clicked="OnExportCsv"/>
        </HorizontalStackLayout>
      </Grid>

      <!-- PAGE BODY -->
      <ScrollView Grid.Row="1">
        <VerticalStackLayout Padding="32,28" Spacing="0">

          <!-- KPI Cards (.kpi-grid 3-col) -->
          <Grid ColumnDefinitions="*,*,*" ColumnSpacing="16" Margin="0,0,0,24">

            <!-- Shelters KPI -->
            <Frame Grid.Column="0" Style="{StaticResource KpiCard}">
              <VerticalStackLayout Spacing="0">
                <Grid ColumnDefinitions="*,Auto" Margin="0,0,0,8">
                  <Label Grid.Column="0" Text="SHELTERS" Style="{StaticResource KpiLabel}"/>
                  <Label Grid.Column="1" Text="🏛️" FontSize="22"/>
                </Grid>
                <Label Text="{Binding Summary.TotalShelters}" Style="{StaticResource KpiValue}"/>
                <Label Text="{Binding Summary.ShelterNote}"   Style="{StaticResource KpiNote}"/>
              </VerticalStackLayout>
            </Frame>

            <!-- Occupancy Rate KPI -->
            <Frame Grid.Column="1" Style="{StaticResource KpiCard}">
              <VerticalStackLayout Spacing="0">
                <Grid ColumnDefinitions="*,Auto" Margin="0,0,0,8">
                  <Label Grid.Column="0" Text="OCCUPANCY RATE" Style="{StaticResource KpiLabel}"/>
                  <Label Grid.Column="1" Text="📊" FontSize="22"/>
                </Grid>
                <Label Text="{Binding Summary.OccupancyRate, StringFormat='{0:F1}%'}"
                       Style="{StaticResource KpiValue}"/>
                <Label Text="{Binding Summary.OccupancyNote}" Style="{StaticResource KpiNote}"/>
              </VerticalStackLayout>
            </Frame>

            <!-- Expiring Items KPI -->
            <Frame Grid.Column="2" Style="{StaticResource KpiCard}">
              <VerticalStackLayout Spacing="0">
                <Grid ColumnDefinitions="*,Auto" Margin="0,0,0,8">
                  <Label Grid.Column="0" Text="EXPIRING ITEMS" Style="{StaticResource KpiLabel}"/>
                  <Label Grid.Column="1" Text="⚠️" FontSize="22"/>
                </Grid>
                <Label Text="{Binding Summary.ExpiringCount}"
                       Style="{StaticResource KpiValue}"
                       TextColor="{StaticResource Red}"/>
                <Label Text="Within the next 30 days" Style="{StaticResource KpiNote}"/>
              </VerticalStackLayout>
            </Frame>

          </Grid>

          <!-- ── SECTION: SHELTER REPORT ── -->
          <!-- Section divider line + label -->
          <Grid ColumnDefinitions="Auto,*" Margin="0,0,0,16">
            <Label Grid.Column="0" Text="SHELTER REPORT"
                   Style="{StaticResource SectionDivider}"/>
            <BoxView Grid.Column="1" HeightRequest="1"
                     Color="{StaticResource Border}"
                     VerticalOptions="Center" Margin="12,0,0,0"/>
          </Grid>

          <Frame Style="{StaticResource Card}" Margin="0,0,0,24">
            <VerticalStackLayout>
              <Grid Padding="18,18,18,0" ColumnDefinitions="*">
                <Label Text="Evacuation Shelter Summary" Style="{StaticResource CardTitle}"/>
                <Label Text="Full status and capacity breakdown"
                       Style="{StaticResource CardSubtitle}" Margin="0,2,0,0"/>
              </Grid>
              <BoxView HeightRequest="1" Color="{StaticResource Border}" Margin="0,14,0,0"/>
              <!-- Table header -->
              <Grid ColumnDefinitions="2*,*,*,*,2*,*,*"
                    Padding="16,12"
                    BackgroundColor="{StaticResource TableHeaderBg}">
                <Label Text="SHELTER NAME"  Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="1" Text="MAX CAP."   Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="2" Text="CURRENT"    Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="3" Text="AVAILABLE"  Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="4" Text="OCCUPANCY"  Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="5" Text="% FULL"     Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="6" Text="STATUS"     Style="{StaticResource TableHeader}"/>
              </Grid>
              <BoxView HeightRequest="1" Color="{StaticResource Border}"/>
              <CollectionView ItemsSource="{Binding ShelterRows}">
                <CollectionView.ItemTemplate>
                  <DataTemplate>
                    <Grid ColumnDefinitions="2*,*,*,*,2*,*,*"
                          Padding="16,0"
                          BackgroundColor="{Binding RowBg}">
                      <Label Text="{Binding Name}"    Style="{StaticResource TableCellBold}"/>
                      <Label Grid.Column="1" Text="{Binding Max}"       Style="{StaticResource TableCell}"/>
                      <Label Grid.Column="2" Text="{Binding Current}"   Style="{StaticResource TableCell}"/>
                      <Label Grid.Column="3" Text="{Binding Available}"
                             Style="{StaticResource TableCell}"
                             TextColor="{Binding AvailableColor}"/>
                      <!-- Progress bar column -->
                      <Grid Grid.Column="4" ColumnDefinitions="*,52" VerticalOptions="Center" Margin="0,13">
                        <Grid Grid.Column="0" HeightRequest="6" Margin="0,0,8,0">
                          <BoxView BackgroundColor="{StaticResource Border}"
                                   CornerRadius="3" HeightRequest="6"/>
                          <BoxView BackgroundColor="{Binding FillColor}"
                                   CornerRadius="3" HeightRequest="6"
                                   HorizontalOptions="Start"
                                   WidthRequest="{Binding BarWidth}"/>
                        </Grid>
                        <Label Grid.Column="1" Text="{Binding PctDisplay}"
                               FontSize="12" TextColor="{StaticResource TextMuted}"
                               VerticalOptions="Center"/>
                      </Grid>
                      <Label Grid.Column="5" Text="{Binding PctFull}"
                             Style="{StaticResource TableCell}"
                             TextColor="{Binding PctColor}"
                             FontAttributes="Bold"/>
                      <Frame Grid.Column="6"
                             BackgroundColor="{Binding StatusBadgeBg}"
                             BorderColor="Transparent" CornerRadius="20"
                             Padding="10,3" HasShadow="False"
                             HorizontalOptions="Start" VerticalOptions="Center">
                        <Label Text="{Binding Status}"
                               FontSize="11" FontAttributes="Bold"
                               TextColor="{Binding StatusTextColor}"/>
                      </Frame>
                      <BoxView Grid.ColumnSpan="7" HeightRequest="1"
                               Color="{StaticResource Border}" VerticalOptions="End"/>
                    </Grid>
                  </DataTemplate>
                </CollectionView.ItemTemplate>
                <!-- Totals footer row -->
                <CollectionView.Footer>
                  <Grid ColumnDefinitions="2*,*,*,*,2*,*,*"
                        Padding="16,13"
                        BackgroundColor="{StaticResource TableHeaderBg}">
                    <Label Text="TOTAL" FontSize="13" FontAttributes="Bold"
                           TextColor="{StaticResource TextSecondary}"/>
                    <Label Grid.Column="1" Text="{Binding ShelterTotals.TotalMax}"
                           FontSize="13" FontAttributes="Bold"/>
                    <Label Grid.Column="2" Text="{Binding ShelterTotals.TotalCurrent}"
                           FontSize="13" FontAttributes="Bold"/>
                    <Label Grid.Column="3" Text="{Binding ShelterTotals.TotalAvailable}"
                           FontSize="13" FontAttributes="Bold"
                           TextColor="{StaticResource Teal}"/>
                    <Label Grid.Column="5" Text="{Binding ShelterTotals.OverallPct}"
                           FontSize="13" FontAttributes="Bold"
                           TextColor="{StaticResource Accent}"/>
                    <Label Grid.Column="6" Text="—" FontSize="13"
                           TextColor="{StaticResource TextMuted}"/>
                  </Grid>
                </CollectionView.Footer>
              </CollectionView>
            </VerticalStackLayout>
          </Frame>

          <!-- ── SECTION: FOOD INVENTORY REPORT ── -->
          <Grid ColumnDefinitions="Auto,*" Margin="0,0,0,16">
            <Label Grid.Column="0" Text="FOOD INVENTORY REPORT"
                   Style="{StaticResource SectionDivider}"/>
            <BoxView Grid.Column="1" HeightRequest="1"
                     Color="{StaticResource Border}"
                     VerticalOptions="Center" Margin="12,0,0,0"/>
          </Grid>

          <Frame Style="{StaticResource Card}" Margin="0,0,0,24">
            <VerticalStackLayout>
              <Grid Padding="18,18,18,0" ColumnDefinitions="*,Auto">
                <VerticalStackLayout Grid.Column="0">
                  <Label Text="🍚 Food Items"         Style="{StaticResource CardTitle}"/>
                  <Label Text="Current stock levels and expiration"
                         Style="{StaticResource CardSubtitle}"/>
                </VerticalStackLayout>
                <Button Grid.Column="1" Text="View Full Inventory"
                        Style="{StaticResource BtnGhostSm}"
                        VerticalOptions="Center"
                        Clicked="OnViewInventory"/>
              </Grid>
              <BoxView HeightRequest="1" Color="{StaticResource Border}" Margin="0,14,0,0"/>
              <Grid ColumnDefinitions="2*,*,*,*,*"
                    Padding="16,12"
                    BackgroundColor="{StaticResource TableHeaderBg}">
                <Label Text="ITEM NAME"      Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="1" Text="QTY"        Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="2" Text="EXPIRATION" Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="3" Text="DAYS LEFT"  Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="4" Text="STATUS"     Style="{StaticResource TableHeader}"/>
              </Grid>
              <BoxView HeightRequest="1" Color="{StaticResource Border}"/>
              <CollectionView ItemsSource="{Binding FoodReportRows}">
                <CollectionView.ItemTemplate>
                  <DataTemplate>
                    <Grid ColumnDefinitions="2*,*,*,*,*"
                          Padding="16,0"
                          BackgroundColor="{Binding RowBg}">
                      <Label Text="{Binding Name}"         Style="{StaticResource TableCellBold}"/>
                      <Label Grid.Column="1" Text="{Binding Qty}"
                             Style="{StaticResource TableCellBold}"/>
                      <Label Grid.Column="2" Text="{Binding ExpiryDisplay}"
                             Style="{StaticResource TableCell}"
                             TextColor="{Binding ExpiryColor}"/>
                      <!-- Days Left badge -->
                      <Frame Grid.Column="3"
                             BackgroundColor="{Binding DaysBadgeBg}"
                             BorderColor="Transparent" CornerRadius="20"
                             Padding="10,3" HasShadow="False"
                             HorizontalOptions="Start" VerticalOptions="Center">
                        <Label Text="{Binding DaysLeft}"
                               FontSize="11" FontAttributes="Bold"
                               TextColor="{Binding DaysBadgeText}"/>
                      </Frame>
                      <!-- Status badge -->
                      <Frame Grid.Column="4"
                             BackgroundColor="{Binding StatusBadgeBg}"
                             BorderColor="Transparent" CornerRadius="20"
                             Padding="10,3" HasShadow="False"
                             HorizontalOptions="Start" VerticalOptions="Center">
                        <Label Text="{Binding StatusLabel}"
                               FontSize="11" FontAttributes="Bold"
                               TextColor="{Binding StatusTextColor}"/>
                      </Frame>
                      <BoxView Grid.ColumnSpan="5" HeightRequest="1"
                               Color="{StaticResource Border}" VerticalOptions="End"/>
                    </Grid>
                  </DataTemplate>
                </CollectionView.ItemTemplate>
                <!-- Food totals row -->
                <CollectionView.Footer>
                  <Grid ColumnDefinitions="2*,*,*,*,*"
                        Padding="16,13"
                        BackgroundColor="{StaticResource TableHeaderBg}">
                    <Label Text="TOTAL" FontSize="13" FontAttributes="Bold"
                           TextColor="{StaticResource TextSecondary}"/>
                    <Label Grid.Column="1"
                           Text="{Binding FoodTotal}"
                           FontSize="13" FontAttributes="Bold"/>
                  </Grid>
                </CollectionView.Footer>
              </CollectionView>
            </VerticalStackLayout>
          </Frame>

          <!-- ── SECTION: MEDICAL INVENTORY REPORT ── -->
          <Grid ColumnDefinitions="Auto,*" Margin="0,0,0,16">
            <Label Grid.Column="0" Text="MEDICAL INVENTORY REPORT"
                   Style="{StaticResource SectionDivider}"/>
            <BoxView Grid.Column="1" HeightRequest="1"
                     Color="{StaticResource Border}"
                     VerticalOptions="Center" Margin="12,0,0,0"/>
          </Grid>

          <Frame Style="{StaticResource Card}" Margin="0,0,0,24">
            <VerticalStackLayout>
              <Grid Padding="18,18,18,0" ColumnDefinitions="*,Auto">
                <VerticalStackLayout Grid.Column="0">
                  <Label Text="💊 Medical Supplies" Style="{StaticResource CardTitle}"/>
                  <Label Text="Current medical stock" Style="{StaticResource CardSubtitle}"/>
                </VerticalStackLayout>
                <Button Grid.Column="1" Text="View Full Inventory"
                        Style="{StaticResource BtnGhostSm}"
                        VerticalOptions="Center"
                        Clicked="OnViewInventory"/>
              </Grid>
              <BoxView HeightRequest="1" Color="{StaticResource Border}" Margin="0,14,0,0"/>
              <Grid ColumnDefinitions="2*,*,*,*"
                    Padding="16,12"
                    BackgroundColor="{StaticResource TableHeaderBg}">
                <Label Text="ITEM NAME"    Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="1" Text="QTY"          Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="2" Text="DOSAGE"       Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="3" Text="RX REQUIRED"  Style="{StaticResource TableHeader}"/>
              </Grid>
              <BoxView HeightRequest="1" Color="{StaticResource Border}"/>
              <CollectionView ItemsSource="{Binding MedReportRows}">
                <CollectionView.ItemTemplate>
                  <DataTemplate>
                    <Grid ColumnDefinitions="2*,*,*,*" Padding="16,0">
                      <Label Text="{Binding Name}"   Style="{StaticResource TableCellBold}"/>
                      <Label Grid.Column="1" Text="{Binding Qty}"
                             Style="{StaticResource TableCellBold}"/>
                      <Label Grid.Column="2" Text="{Binding Dosage}"
                             Style="{StaticResource TableCellMuted}"/>
                      <Frame Grid.Column="3"
                             BackgroundColor="{Binding RxBadgeBg}"
                             BorderColor="Transparent" CornerRadius="20"
                             Padding="10,3" HasShadow="False"
                             HorizontalOptions="Start" VerticalOptions="Center">
                        <Label Text="{Binding RxLabel}"
                               FontSize="11" FontAttributes="Bold"
                               TextColor="{Binding RxTextColor}"/>
                      </Frame>
                      <BoxView Grid.ColumnSpan="4" HeightRequest="1"
                               Color="{StaticResource Border}" VerticalOptions="End"/>
                    </Grid>
                  </DataTemplate>
                </CollectionView.ItemTemplate>
                <CollectionView.Footer>
                  <Grid ColumnDefinitions="2*,*,*,*"
                        Padding="16,13"
                        BackgroundColor="{StaticResource TableHeaderBg}">
                    <Label Text="TOTAL" FontSize="13" FontAttributes="Bold"
                           TextColor="{StaticResource TextSecondary}"/>
                    <Label Grid.Column="1"
                           Text="{Binding MedTotal}"
                           FontSize="13" FontAttributes="Bold"/>
                  </Grid>
                </CollectionView.Footer>
              </CollectionView>
            </VerticalStackLayout>
          </Frame>

          <!-- ── SECTION: EXPIRING ALERT ── -->
          <Grid ColumnDefinitions="Auto,*" Margin="0,0,0,16">
            <Label Grid.Column="0" Text="EXPIRING FOOD ALERT"
                   Style="{StaticResource SectionDivider}"/>
            <BoxView Grid.Column="1" HeightRequest="1"
                     Color="{StaticResource Border}"
                     VerticalOptions="Center" Margin="12,0,0,0"/>
          </Grid>

          <Frame Style="{StaticResource AlertDanger}" Margin="0,0,0,16">
            <HorizontalStackLayout Spacing="12">
              <Label Text="⚠️" FontSize="14" VerticalOptions="Center"/>
              <Label VerticalOptions="Center" FontSize="13" TextColor="#991B1B">
                <Label.FormattedText>
                  <FormattedString>
                    <Span Text="{Binding Summary.ExpiringCount}" FontAttributes="Bold"/>
                    <Span Text=" item(s) are expiring within 30 days. Immediate review required."/>
                  </FormattedString>
                </Label.FormattedText>
              </Label>
            </HorizontalStackLayout>
          </Frame>

          <Frame Style="{StaticResource Card}" Margin="0,0,0,0">
            <VerticalStackLayout>
              <Grid Padding="18,18,18,0">
                <Label Text="⚠ Expiring Items (within 30 days)" Style="{StaticResource CardTitle}"/>
                <Label Text="These items need urgent attention"
                       FontSize="12" TextColor="{StaticResource Red}" Margin="0,2,0,0"/>
              </Grid>
              <BoxView HeightRequest="1" Color="{StaticResource Border}" Margin="0,14,0,0"/>
              <Grid ColumnDefinitions="2*,*,*,*"
                    Padding="16,12"
                    BackgroundColor="{StaticResource TableHeaderBg}">
                <Label Text="FOOD ITEM"   Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="1" Text="QUANTITY"   Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="2" Text="EXPIRES ON" Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="3" Text="DAYS LEFT"  Style="{StaticResource TableHeader}"/>
              </Grid>
              <BoxView HeightRequest="1" Color="{StaticResource Border}"/>
              <CollectionView ItemsSource="{Binding ExpiringItems}">
                <CollectionView.ItemTemplate>
                  <DataTemplate>
                    <Grid ColumnDefinitions="2*,*,*,*"
                          Padding="16,0"
                          BackgroundColor="{Binding RowBg}">
                      <Label Text="{Binding Name}"    Style="{StaticResource TableCellBold}"/>
                      <Label Grid.Column="1" Text="{Binding Qty}"
                             Style="{StaticResource TableCellBold}"/>
                      <Label Grid.Column="2" Text="{Binding ExpiryDisplay}"
                             Style="{StaticResource TableCell}"
                             TextColor="{Binding ExpiryColor}"/>
                      <Frame Grid.Column="3"
                             BackgroundColor="{Binding DaysBadgeBg}"
                             BorderColor="Transparent" CornerRadius="20"
                             Padding="10,3" HasShadow="False"
                             HorizontalOptions="Start" VerticalOptions="Center">
                        <Label Text="{Binding DaysLeft}"
                               FontSize="11" FontAttributes="Bold"
                               TextColor="{Binding DaysBadgeText}"/>
                      </Frame>
                      <BoxView Grid.ColumnSpan="4" HeightRequest="1"
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
  </Grid>
</ContentPage>
