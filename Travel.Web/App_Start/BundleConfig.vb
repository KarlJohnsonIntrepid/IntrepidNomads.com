Public Class BundleConfig

    Public Shared Sub RegisterBundles(bundles As BundleCollection)
        bundles.Add(
            New ScriptBundle(
                "~/bundles/Scripts").Include(
                "~/scripts/jquery-1.9.1.min.js",
                "~/scripts/jquery-ui-1.8.24.min.js",
                "~/scripts/bootstrap.min.js",
                "~/scripts/jquery.dotdotdot.min.js",
                "~/content/fancybox/lib/jquery.mousewheel-3.0.6.pack.js",
                "~/content/fancybox/source/jquery.fancybox.pack.js",
                "~/content/fancybox/source/helpers/jquery.fancybox-buttons.js",
                "~/content/fancybox/source/helpers/jquery.fancybox-media.js",
                "~/content/fancybox/source/helpers/jquery.fancybox-thumbs.js",
                "~/scripts/main.js",
                "~/scripts/paralax.js"))
    End Sub
End Class

