
imports TrackSpy
Public Class Application
 	Private Shared Render As New TrackSpy.Renderers.RenderVideo
	Public Shared Sub Main()
		System.Console.WriteLine("Hello world!")

		dim Layout as string ="C:\Program Files (x86)\Microsoft\TrackRenderV2Setup\dials\testconfig1.xml"
		dim Tmp as string  ="C:\Temp\data"
		dim RecordingID as integer=40
		dim Ffmpg as string ="C:\ffmpeg-20150312-git-3bedc99-win32-static\bin"

		Render.Render(Layout,RecordingID,Tmp, Ffmpg)

		System.Console.WriteLine("Finished")
	End Sub
End Class

