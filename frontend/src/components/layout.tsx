import SideNav from "./sidenav";
import { Outlet } from "react-router-dom";

export default function Layout()
{
    return (
        <>
            {/* <div className="block md:w-screen md:h-1/6">
                Top nav
            </div> */}
            <div className="flex">
                {/* <div className="hidden lg:block md:w-1/6 md:h-5/6">
                    <SideNav />
                </div> */}
                {/* className="md:w-5/6 md:h-5/6" */}
                <main className="w-full h-full">
                    <Outlet />
                </main>
            </div>
        </>
    );
}