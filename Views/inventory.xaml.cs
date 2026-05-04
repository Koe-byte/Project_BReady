<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="ProjectBReady.Views.inventory"
             BackgroundColor="{StaticResource MainBg}">

  <Grid ColumnDefinitions="260,*">

    <!-- ═══════════════════════════════════════
         SIDEBAR
    ═══════════════════════════════════════ -->
    <Grid Grid.Column="0"
          RowDefinitions="Auto,*,Auto"
          BackgroundColor="{StaticResource SidebarBg}">

      <!-- Brand -->
      <VerticalStackLayout Grid.Row="0" Padding="24,28,24,20" Spacing="0">
        <Frame BackgroundColor="{StaticResource Accent}"
               CornerRadius="10" WidthRequest="40" HeightRequest="40"
               Padding="0" HasShadow="False" BorderColor="Transparent"
               HorizontalOptions="Start" Margin="0,0,0,12">
          <Label Text="🛡️" FontSize="20"
                 HorizontalOptions="Center" VerticalOptions="Center"/>
        </Frame>
        <Label Text="b-ready" TextColor="White" FontSize="17" FontAttributes="Bold"/>
        <Label Text="BARANGAY DISASTER RELIEF"
               TextColor="{StaticResource TextMuted}"
               FontSize="10" CharacterSpacing="1" Margin="0,3,0,0"/>
        <BoxView HeightRequest="1" Color="#0F172A" Margin="0,20,0,0" Opacity="0.5"/>
      </VerticalStackLayout>

      <!-- Nav -->
      <VerticalStackLayout Grid.Row="1" Padding="12,16" Spacing="0">
        <Label Text="MAIN" Style="{StaticResource NavSectionLabel}"/>
        <Button Text="📊  Dashboard"  Style="{StaticResource NavItem}"    Clicked="OnNavDashboard"/>
        <Button Text="🏛️  Shelters"   Style="{StaticResource NavItem}"    Clicked="OnNavShelters"/>
        <Button Text="📦  Inventory"  Style="{StaticResource NavItemActive}" Clicked="OnNavInventory"/>
        <Button Text="📋  Reports"    Style="{StaticResource NavItem}"    Clicked="OnNavReports"/>
      </VerticalStackLayout>

      <!-- Admin footer -->
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
          <Label Text="Inventory Management" Style="{StaticResource PageH1}"/>
          <Label Text="Track food supplies and medical goods" Style="{StaticResource PageSubtitle}"/>
        </VerticalStackLayout>
        <HorizontalStackLayout Grid.Column="1" VerticalOptions="Center" Spacing="10">
          <Button Text="⟳  Refresh"  Style="{StaticResource BtnGhost}"   Clicked="OnRefresh"/>
          <Button Text="+ Add Item"   Style="{StaticResource BtnPrimary}" Clicked="OnAddItem"/>
        </HorizontalStackLayout>
      </Grid>

      <!-- PAGE BODY -->
      <ScrollView Grid.Row="1">
        <VerticalStackLayout Padding="32,28" Spacing="0">

          <!-- Alert warn -->
          <Frame Style="{StaticResource AlertWarn}" Margin="0,0,0,20">
            <HorizontalStackLayout Spacing="12">
              <Label Text="⚠️" VerticalOptions="Center" FontSize="14"/>
              <Label VerticalOptions="Center" FontSize="13" TextColor="#92400E">
                <Label.FormattedText>
                  <FormattedString>
                    <Span Text="3 food items" FontAttributes="Bold"/>
                    <Span Text=" are expiring within 7 days — Canned Sardines, Bottled Water (500ml), Rice (25kg sack). "/>
                    <Span Text="View in Reports →" FontAttributes="Bold"/>
                  </FormattedString>
                </Label.FormattedText>
              </Label>
            </HorizontalStackLayout>
          </Frame>

          <!-- Stat Cards (.stat-grid 4-col) -->
          <Grid ColumnDefinitions="*,*,*,*" ColumnSpacing="16" Margin="0,0,0,28">
            <Frame Grid.Column="0" Style="{StaticResource StatCard}">
              <VerticalStackLayout>
                <Frame BackgroundColor="{StaticResource BlueLight}"
                       CornerRadius="12" WidthRequest="44" HeightRequest="44"
                       Padding="0" HasShadow="False" BorderColor="Transparent"
                       HorizontalOptions="Start" Margin="0,0,0,14">
                  <Label Text="🍚" FontSize="20" HorizontalOptions="Center" VerticalOptions="Center"/>
                </Frame>
                <Label Text="{Binding FoodItemCount}" Style="{StaticResource StatValue}"/>
                <Label Text="Food Item Types" Style="{StaticResource StatLabel}"/>
                <BoxView HeightRequest="3" CornerRadius="2" Margin="0,12,-24,-22"
                         BackgroundColor="{StaticResource Blue}"/>
              </VerticalStackLayout>
            </Frame>
            <Frame Grid.Column="1" Style="{StaticResource StatCard}">
              <VerticalStackLayout>
                <Frame BackgroundColor="{StaticResource AccentLight}"
                       CornerRadius="12" WidthRequest="44" HeightRequest="44"
                       Padding="0" HasShadow="False" BorderColor="Transparent"
                       HorizontalOptions="Start" Margin="0,0,0,14">
                  <Label Text="📦" FontSize="20" HorizontalOptions="Center" VerticalOptions="Center"/>
                </Frame>
                <Label Text="{Binding TotalFoodUnits}" Style="{StaticResource StatValue}"/>
                <Label Text="Total Food Units" Style="{StaticResource StatLabel}"/>
                <BoxView HeightRequest="3" CornerRadius="2" Margin="0,12,-24,-22"
                         BackgroundColor="{StaticResource Accent}"/>
              </VerticalStackLayout>
            </Frame>
            <Frame Grid.Column="2" Style="{StaticResource StatCard}">
              <VerticalStackLayout>
                <Frame BackgroundColor="{StaticResource TealLight}"
                       CornerRadius="12" WidthRequest="44" HeightRequest="44"
                       Padding="0" HasShadow="False" BorderColor="Transparent"
                       HorizontalOptions="Start" Margin="0,0,0,14">
                  <Label Text="💊" FontSize="20" HorizontalOptions="Center" VerticalOptions="Center"/>
                </Frame>
                <Label Text="{Binding MedItemCount}" Style="{StaticResource StatValue}"/>
                <Label Text="Medical Item Types" Style="{StaticResource StatLabel}"/>
                <BoxView HeightRequest="3" CornerRadius="2" Margin="0,12,-24,-22"
                         BackgroundColor="{StaticResource Teal}"/>
              </VerticalStackLayout>
            </Frame>
            <Frame Grid.Column="3" Style="{StaticResource StatCard}">
              <VerticalStackLayout>
                <Frame BackgroundColor="{StaticResource RedLight}"
                       CornerRadius="12" WidthRequest="44" HeightRequest="44"
                       Padding="0" HasShadow="False" BorderColor="Transparent"
                       HorizontalOptions="Start" Margin="0,0,0,14">
                  <Label Text="🩺" FontSize="20" HorizontalOptions="Center" VerticalOptions="Center"/>
                </Frame>
                <Label Text="{Binding TotalMedUnits}" Style="{StaticResource StatValue}"/>
                <Label Text="Total Medical Units" Style="{StaticResource StatLabel}"/>
                <BoxView HeightRequest="3" CornerRadius="2" Margin="0,12,-24,-22"
                         BackgroundColor="{StaticResource Red}"/>
              </VerticalStackLayout>
            </Frame>
          </Grid>

          <!-- TABS (.tabs) — implemented via BindableLayout toggle -->
          <HorizontalStackLayout Spacing="0" Margin="0,0,0,0">
            <Button x:Name="TabFoodBtn"
                    Text="🍚  Food Items"
                    BackgroundColor="Transparent"
                    TextColor="{StaticResource Accent}"
                    FontSize="14" FontAttributes="Bold"
                    Padding="20,10"
                    BorderColor="{StaticResource Accent}"
                    BorderWidth="0"
                    Clicked="OnTabFood"/>
            <Button x:Name="TabMedBtn"
                    Text="💊  Medical Supplies"
                    BackgroundColor="Transparent"
                    TextColor="{StaticResource TextMuted}"
                    FontSize="14"
                    Padding="20,10"
                    BorderWidth="0"
                    Clicked="OnTabMedical"/>
          </HorizontalStackLayout>
          <!-- Tab underline -->
          <BoxView x:Name="TabUnderline"
                   HeightRequest="2"
                   Color="{StaticResource Accent}"
                   Margin="0,0,0,20"
                   HorizontalOptions="Start"
                   WidthRequest="130"/>

          <!-- ── FOOD TAB PANE ── -->
          <Frame x:Name="FoodPane" Style="{StaticResource Card}" Margin="0,0,0,0">
            <VerticalStackLayout>
              <!-- Toolbar -->
              <Grid Padding="14,14" BackgroundColor="{StaticResource ToolbarBg}"
                    ColumnDefinitions="Auto,Auto,Auto,Auto,Auto,*,Auto">
                <BoxView Grid.ColumnSpan="7" HeightRequest="1"
                         Color="{StaticResource Border}" VerticalOptions="End"/>
                <!-- Search -->
                <Frame Grid.Column="0" Style="{StaticResource SearchBoxFrame}"
                       WidthRequest="200">
                  <HorizontalStackLayout Spacing="8">
                    <Label Text="🔍" TextColor="{StaticResource TextMuted}" VerticalOptions="Center"/>
                    <Entry Placeholder="Search food items…"
                           PlaceholderColor="{StaticResource TextMuted}"
                           TextColor="{StaticResource TextPrimary}"
                           FontSize="13" BackgroundColor="Transparent"
                           TextChanged="OnFoodSearchChanged"/>
                  </HorizontalStackLayout>
                </Frame>
                <BoxView Grid.Column="1" WidthRequest="1" HeightRequest="24"
                         Color="{StaticResource Border}" VerticalOptions="Center" Margin="8,0"/>
                <Button Grid.Column="2" Text="📤 Dispatch"   Style="{StaticResource BtnGhostSm}" Clicked="OnDispatchFood"/>
                <Button Grid.Column="3" Text="📥 Stock In"   Style="{StaticResource BtnGhostSm}" Clicked="OnStockInFood"/>
                <Button Grid.Column="4" Text="🗑 Delete"     Style="{StaticResource BtnGhostSmDanger}" Clicked="OnDeleteFood"/>
                <Button Grid.Column="6" Text="+ Add Food"    Style="{StaticResource BtnPrimary}"
                        FontSize="12" Padding="10,5" Clicked="OnAddFood"/>
              </Grid>
              <!-- Food Table Header -->
              <Grid ColumnDefinitions="36,2*,*,*,*,*"
                    Padding="16,12"
                    BackgroundColor="{StaticResource TableHeaderBg}">
                <Label Grid.Column="1" Text="ITEM NAME"        Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="2" Text="QUANTITY"         Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="3" Text="EXPIRATION DATE"  Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="4" Text="STATUS"           Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="5" Text="ACTIONS"          Style="{StaticResource TableHeader}"/>
              </Grid>
              <BoxView HeightRequest="1" Color="{StaticResource Border}"/>
              <!-- Food Rows -->
              <CollectionView x:Name="FoodCollectionView"
                              ItemsSource="{Binding FoodItems}"
                              SelectionMode="Single"
                              SelectionChanged="OnFoodSelectionChanged">
                <CollectionView.ItemTemplate>
                  <DataTemplate>
                    <Grid ColumnDefinitions="36,2*,*,*,*,*"
                          Padding="16,0"
                          BackgroundColor="{Binding RowBg}">
                      <RadioButton Grid.Column="0" VerticalOptions="Center"/>
                      <Label Grid.Column="1" Text="{Binding Name}" Style="{StaticResource TableCellBold}"/>
                      <Label Grid.Column="2" Text="{Binding Qty}"  Style="{StaticResource TableCellBold}"/>
                      <Label Grid.Column="3" Text="{Binding ExpiryDisplay}" Style="{StaticResource TableCellMuted}"/>
                      <Frame Grid.Column="4"
                             BackgroundColor="{Binding StatusBadgeBg}"
                             BorderColor="Transparent" CornerRadius="20"
                             Padding="10,3" HasShadow="False"
                             HorizontalOptions="Start" VerticalOptions="Center">
                        <Label Text="{Binding StatusLabel}"
                               FontSize="11" FontAttributes="Bold"
                               TextColor="{Binding StatusTextColor}"/>
                      </Frame>
                      <Button Grid.Column="5" Text="✏️ Edit"
                              Style="{StaticResource BtnGhostSm}"
                              Clicked="OnEditFoodItem"
                              CommandParameter="{Binding .}"/>
                      <BoxView Grid.ColumnSpan="6" HeightRequest="1"
                               Color="{StaticResource Border}" VerticalOptions="End"/>
                    </Grid>
                  </DataTemplate>
                </CollectionView.ItemTemplate>
              </CollectionView>
            </VerticalStackLayout>
          </Frame>

          <!-- ── MEDICAL TAB PANE ── -->
          <Frame x:Name="MedPane" Style="{StaticResource Card}"
                 IsVisible="False" Margin="0,0,0,0">
            <VerticalStackLayout>
              <!-- Toolbar -->
              <Grid Padding="14,14" BackgroundColor="{StaticResource ToolbarBg}"
                    ColumnDefinitions="Auto,Auto,Auto,Auto,Auto,*,Auto">
                <BoxView Grid.ColumnSpan="7" HeightRequest="1"
                         Color="{StaticResource Border}" VerticalOptions="End"/>
                <Frame Grid.Column="0" Style="{StaticResource SearchBoxFrame}" WidthRequest="200">
                  <HorizontalStackLayout Spacing="8">
                    <Label Text="🔍" TextColor="{StaticResource TextMuted}" VerticalOptions="Center"/>
                    <Entry Placeholder="Search medical supplies…"
                           PlaceholderColor="{StaticResource TextMuted}"
                           TextColor="{StaticResource TextPrimary}"
                           FontSize="13" BackgroundColor="Transparent"
                           TextChanged="OnMedSearchChanged"/>
                  </HorizontalStackLayout>
                </Frame>
                <BoxView Grid.Column="1" WidthRequest="1" HeightRequest="24"
                         Color="{StaticResource Border}" VerticalOptions="Center" Margin="8,0"/>
                <Button Grid.Column="2" Text="📤 Dispatch"  Style="{StaticResource BtnGhostSm}"       Clicked="OnDispatchMed"/>
                <Button Grid.Column="3" Text="📥 Stock In"  Style="{StaticResource BtnGhostSm}"       Clicked="OnStockInMed"/>
                <Button Grid.Column="4" Text="🗑 Delete"    Style="{StaticResource BtnGhostSmDanger}" Clicked="OnDeleteMed"/>
                <Button Grid.Column="6" Text="+ Add Medical" Style="{StaticResource BtnPrimary}"
                        FontSize="12" Padding="10,5" Clicked="OnAddMedical"/>
              </Grid>
              <!-- Medical Table Header -->
              <Grid ColumnDefinitions="36,2*,*,*,*,*"
                    Padding="16,12"
                    BackgroundColor="{StaticResource TableHeaderBg}">
                <Label Grid.Column="1" Text="ITEM NAME"    Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="2" Text="QUANTITY"     Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="3" Text="DOSAGE"       Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="4" Text="RX REQUIRED"  Style="{StaticResource TableHeader}"/>
                <Label Grid.Column="5" Text="ACTIONS"      Style="{StaticResource TableHeader}"/>
              </Grid>
              <BoxView HeightRequest="1" Color="{StaticResource Border}"/>
              <!-- Medical Rows -->
              <CollectionView x:Name="MedCollectionView"
                              ItemsSource="{Binding MedItems}"
                              SelectionMode="Single"
                              SelectionChanged="OnMedSelectionChanged">
                <CollectionView.ItemTemplate>
                  <DataTemplate>
                    <Grid ColumnDefinitions="36,2*,*,*,*,*" Padding="16,0">
                      <RadioButton Grid.Column="0" VerticalOptions="Center"/>
                      <Label Grid.Column="1" Text="{Binding Name}"   Style="{StaticResource TableCellBold}"/>
                      <Label Grid.Column="2" Text="{Binding Qty}"    Style="{StaticResource TableCellBold}"/>
                      <Label Grid.Column="3" Text="{Binding Dosage}" Style="{StaticResource TableCellMuted}"/>
                      <Frame Grid.Column="4"
                             BackgroundColor="{Binding RxBadgeBg}"
                             BorderColor="Transparent" CornerRadius="20"
                             Padding="10,3" HasShadow="False"
                             HorizontalOptions="Start" VerticalOptions="Center">
                        <Label Text="{Binding RxLabel}"
                               FontSize="11" FontAttributes="Bold"
                               TextColor="{Binding RxTextColor}"/>
                      </Frame>
                      <Button Grid.Column="5" Text="✏️ Edit"
                              Style="{StaticResource BtnGhostSm}"
                              Clicked="OnEditMedItem"
                              CommandParameter="{Binding .}"/>
                      <BoxView Grid.ColumnSpan="6" HeightRequest="1"
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
         MODALS — layered on top via AbsoluteLayout
         (In production: use MAUI Shell popup or
          a custom overlay ContentView)
    ═══════════════════════════════════════ -->

    <!-- Dispatch Modal -->
    <Grid x:Name="ModalDispatchOverlay"
          Grid.ColumnSpan="2"
          IsVisible="False"
          BackgroundColor="#73000000">
      <Frame Style="{StaticResource ModalCard}"
             HorizontalOptions="Center" VerticalOptions="Center">
        <VerticalStackLayout>
          <!-- Header -->
          <Grid Padding="26,22,26,16" ColumnDefinitions="*,Auto">
            <BoxView Grid.ColumnSpan="2" HeightRequest="1"
                     Color="{StaticResource Border}" VerticalOptions="End"/>
            <VerticalStackLayout Grid.Column="0">
              <Label Text="📤 Dispatch Item" Style="{StaticResource ModalTitle}"/>
              <Label x:Name="DispatchItemName" Style="{StaticResource ModalSub}"/>
            </VerticalStackLayout>
            <Button Grid.Column="1" Text="✕"
                    BackgroundColor="Transparent"
                    TextColor="{StaticResource TextMuted}"
                    FontSize="18" WidthRequest="32" HeightRequest="32"
                    Padding="0" CornerRadius="6"
                    Clicked="OnCloseDispatchModal"/>
          </Grid>
          <!-- Body -->
          <VerticalStackLayout Padding="26,24" Spacing="0">
            <Label Text="Quantity to Dispatch" Style="{StaticResource FormLabel}"/>
            <Frame Style="{StaticResource FormInputFrame}" Margin="0,0,0,8">
              <Entry x:Name="DispatchQtyEntry"
                     Keyboard="Numeric"
                     Placeholder="0"
                     FontSize="14"
                     BackgroundColor="Transparent"
                     TextColor="{StaticResource TextPrimary}"/>
            </Frame>
            <Label x:Name="DispatchAvailLabel"
                   FontSize="12"
                   TextColor="{StaticResource TextMuted}"/>
          </VerticalStackLayout>
          <!-- Footer -->
          <Grid Padding="26,16" ColumnDefinitions="*,Auto,Auto">
            <BoxView Grid.ColumnSpan="3" HeightRequest="1"
                     Color="{StaticResource Border}" VerticalOptions="Start"/>
            <Button Grid.Column="1" Text="Cancel"
                    Style="{StaticResource BtnGhost}"
                    Clicked="OnCloseDispatchModal"/>
            <Button Grid.Column="2" Text="📤 Confirm Dispatch"
                    Style="{StaticResource BtnPrimary}"
                    Margin="10,0,0,0"
                    Clicked="OnConfirmDispatch"/>
          </Grid>
        </VerticalStackLayout>
      </Frame>
    </Grid>

    <!-- Stock In Modal -->
    <Grid x:Name="ModalStockInOverlay"
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
              <Label Text="📥 Stock In" Style="{StaticResource ModalTitle}"/>
              <Label x:Name="StockInItemName" Style="{StaticResource ModalSub}"/>
            </VerticalStackLayout>
            <Button Grid.Column="1" Text="✕"
                    BackgroundColor="Transparent"
                    TextColor="{StaticResource TextMuted}"
                    FontSize="18" WidthRequest="32" HeightRequest="32"
                    Padding="0" CornerRadius="6"
                    Clicked="OnCloseStockInModal"/>
          </Grid>
          <VerticalStackLayout Padding="26,24" Spacing="0">
            <Label Text="Quantity to Add" Style="{StaticResource FormLabel}"/>
            <Frame Style="{StaticResource FormInputFrame}">
              <Entry x:Name="StockInQtyEntry"
                     Keyboard="Numeric" Placeholder="0"
                     FontSize="14" BackgroundColor="Transparent"
                     TextColor="{StaticResource TextPrimary}"/>
            </Frame>
          </VerticalStackLayout>
          <Grid Padding="26,16" ColumnDefinitions="*,Auto,Auto">
            <BoxView Grid.ColumnSpan="3" HeightRequest="1"
                     Color="{StaticResource Border}" VerticalOptions="Start"/>
            <Button Grid.Column="1" Text="Cancel"
                    Style="{StaticResource BtnGhost}"
                    Clicked="OnCloseStockInModal"/>
            <Button Grid.Column="2" Text="📥 Confirm Stock In"
                    Style="{StaticResource BtnTeal}"
                    Margin="10,0,0,0"
                    Clicked="OnConfirmStockIn"/>
          </Grid>
        </VerticalStackLayout>
      </Frame>
    </Grid>

    <!-- Delete Modal -->
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
              <Label Text="Delete Item" Style="{StaticResource ModalTitle}"/>
              <Label Text="This cannot be undone" Style="{StaticResource ModalSub}"/>
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
              <Label x:Name="DeleteConfirmLabel"
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
