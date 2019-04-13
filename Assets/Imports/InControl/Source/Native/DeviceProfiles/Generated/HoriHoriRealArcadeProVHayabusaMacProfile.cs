namespace InControl.NativeProfile
{
	using System;


	// @cond nodoc
	public class HoriHoriRealArcadeProVHayabusaMacProfile : Xbox360DriverMacProfile
	{
		public HoriHoriRealArcadeProVHayabusaMacProfile()
		{
			Name = "Hori Hori Real Arcade Pro V Hayabusa";
			Meta = "Hori Hori Real Arcade Pro V Hayabusa on Mac";

			Matchers = new[] {
				new NativeInputDeviceMatcher {
					VendorID = 0x0f0d,
					ProductID = 0x00d8,
				},
			};
		}
	}
	// @endcond
}


