namespace InControl.NativeProfile
{
	using System;


	// @cond nodoc
	public class HoriHoriRealArcadeProIVMacProfile : Xbox360DriverMacProfile
	{
		public HoriHoriRealArcadeProIVMacProfile()
		{
			Name = "Hori Hori Real Arcade Pro IV";
			Meta = "Hori Hori Real Arcade Pro IV on Mac";

			Matchers = new[] {
				new NativeInputDeviceMatcher {
					VendorID = 0x0f0d,
					ProductID = 0x008c,
				},
			};
		}
	}
	// @endcond
}


