namespace InControl.NativeProfile
{
	using System;


	// @cond nodoc
	public class AtPlayControllerMacProfile : Xbox360DriverMacProfile
	{
		public AtPlayControllerMacProfile()
		{
			Name = "At Play Controller";
			Meta = "At Play Controller on Mac";

			Matchers = new[] {
				new NativeInputDeviceMatcher {
					VendorID = 0x24c6,
					ProductID = 0xfafa,
				},
				new NativeInputDeviceMatcher {
					VendorID = 0x24c6,
					ProductID = 0xfafb,
				},
			};
		}
	}
	// @endcond
}


