Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging

Public Class ImageCompressor

    ''' <summary>
    ''' Shirnk image and compress
    ''' </summary>
    ''' <param name="ImagePath">Path to which the image would be saved.</param> 
    ''' <param name="Quality">An integer from 0 to 100, with 100 being the 
    ''' highest quality</param> 
    ''' <param name="Height"></param>
    ''' <param name="Width"></param>
    ''' <remarks></remarks>
    Public Shared Sub CompressAndShrinkImage(ImagePath As String, Image As Image, Height As Integer, Width As Integer, Quality As Integer)

        ImageCompressor.SaveJpeg(ImagePath, Image, Quality)
        Dim BitMap As Bitmap = ImageCompressor.ResizeImage(ImagePath, Height, Width)
        BitMap.Save(ImagePath)

    End Sub

    Public Shared Function ResizeImage(lcFilename As String, lnWidth As Integer, lnHeight As Integer) As Bitmap

        Dim bmpOut As System.Drawing.Bitmap = Nothing

        Try
            Dim loBMP As New Bitmap(lcFilename)
            Dim loFormat As ImageFormat = loBMP.RawFormat

            Dim lnRatio As Decimal
            Dim lnNewWidth As Integer = 0
            Dim lnNewHeight As Integer = 0

            If loBMP.Width < lnWidth AndAlso loBMP.Height < lnHeight Then
                Return loBMP
            End If

            If loBMP.Width > loBMP.Height Then
                lnRatio = CDec(lnWidth) / loBMP.Width
                lnNewWidth = lnWidth
                Dim lnTemp As Decimal = loBMP.Height * lnRatio
                lnNewHeight = CInt(Math.Truncate(lnTemp))
            Else
                lnRatio = CDec(lnHeight) / loBMP.Height
                lnNewHeight = lnHeight
                Dim lnTemp As Decimal = loBMP.Width * lnRatio
                lnNewWidth = CInt(Math.Truncate(lnTemp))
            End If


            bmpOut = New Bitmap(lnNewWidth, lnNewHeight)
            Dim g As Graphics = Graphics.FromImage(bmpOut)
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality
            g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight)
            g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight)

            loBMP.Dispose()
        Catch
            Return Nothing
        End Try
        Return bmpOut
    End Function


    ''' <summary> 
    ''' Saves an image as a jpeg image, with the given quality 
    ''' </summary> 
    ''' <param name="path">Path to which the image would be saved.</param> 
    ''' <param name="quality">An integer from 0 to 100, with 100 being the 
    ''' highest quality</param> 
    Public Shared Sub SaveJpeg(path As String, img As System.Drawing.Image, quality As Integer)
        If quality < 0 OrElse quality > 100 Then
            Throw New ArgumentOutOfRangeException("quality must be between 0 and 100.")
        End If

        ' Encoder parameter for image quality 
        Dim qualityParam As New EncoderParameter(Encoder.Quality, quality)
        ' Jpeg image codec 
        Dim jpegCodec As ImageCodecInfo = GetEncoderInfo("image/jpeg")

        Dim encoderParams As New EncoderParameters(1)
        encoderParams.Param(0) = qualityParam

        img.Save(path, jpegCodec, encoderParams)
    End Sub

    ''' <summary> 
    ''' Returns the image codec with the given mime type 
    ''' </summary> 
    Private Shared Function GetEncoderInfo(mimeType As String) As ImageCodecInfo
        ' Get image codecs for all image formats 
        Dim codecs As ImageCodecInfo() = ImageCodecInfo.GetImageEncoders()

        ' Find the correct image codec 
        For i As Integer = 0 To codecs.Length - 1
            If codecs(i).MimeType = mimeType Then
                Return codecs(i)
            End If
        Next
        Return Nothing

    End Function

End Class
