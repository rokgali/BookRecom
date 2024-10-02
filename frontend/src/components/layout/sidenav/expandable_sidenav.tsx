import { useState } from "react";
import ClosedSidenav from "./closed_sidenav";
import ExpandedSideNav from "./expanded_sidenav";
import { SidenavProps } from "../../../interfaces/sidenavprops";

export default function ExpandableSidenav(props: SidenavProps)
{
    const [expanded, setExpanded] = useState(false);

    function ToggleExpanded() {
        setExpanded(!expanded);
    }

    return (
        <div className="min-h-full">
            {expanded ? <ExpandedSideNav navigations={props.navigations} toggleExpanded={ToggleExpanded}  /> :  <ClosedSidenav toggleExpanded={ToggleExpanded} />}
        </div>
    );

}