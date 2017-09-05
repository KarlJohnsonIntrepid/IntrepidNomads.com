Public Class Countries
    Inherits BasePage

    Private Sub Home_Init(sender As Object, e As EventArgs) Handles Me.Init
        CType(Me.MyMaster, blog1).HideSideBar()
    End Sub

    Private Sub Countries_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Title = "Destinations - Intrepid Nomads - Budget BackPacking As A Couple"
        Me.Description = "The best budget travel tips from the road. Find out where to go, what to see, and how much it costs. Start planning your own trip now."
        LoadList()
    End Sub

    Private Sub LoadList()

        Dim NorthAmerica = Business.Country.SelectCountriesNotNA.Where(Function(x) x.ContinentDescription = "North America")
        lstNorthAmerica.LoadCountries(NorthAmerica.ToList)

        Dim CentralAmerica = Business.Country.SelectCountriesNotNA.Where(Function(x) x.ContinentDescription = "Central America")
        lstCentralAmerica.LoadCountries(CentralAmerica.ToList)

        Dim SouthAmerica = Business.Country.SelectCountriesNotNA.Where(Function(x) x.ContinentDescription = "South America")
        lstSouthAmerica.LoadCountries(SouthAmerica.ToList)

        Dim Europe = Business.Country.SelectCountriesNotNA.Where(Function(x) x.ContinentDescription = "Europe")
        lstEurope.LoadCountries(Europe.ToList)

        Dim Asia = Business.Country.SelectCountriesNotNA.Where(Function(x) x.ContinentDescription = "Asia")
        lstAsia.LoadCountries(Asia.ToList)

        Dim MiddleEast = Business.Country.SelectCountriesNotNA.Where(Function(x) x.ContinentDescription = "Middle East")
        lstMiddleEast.LoadCountries(MiddleEast.ToList)

        Dim Africa = Business.Country.SelectCountriesNotNA.Where(Function(x) x.ContinentDescription = "Africa")
        lstAfrica.LoadCountries(Africa.ToList)

        Dim Australasia = Business.Country.SelectCountriesNotNA.Where(Function(x) x.ContinentDescription = "Australasia")
        lstaustralasia.LoadCountries(Australasia.ToList)

    End Sub
End Class