<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:mrep="http://www.navitas.nl/2014/Mapper/dbaccess/mrep" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:user="http://www.navitas.nl/2014/Mapper/scripts" xmlns:m="http://www.navitas.nl/2014/Mapper">
  <xsl:template match="/">
    <target>
      <mrep3>
        <xsl:for-each select="mrep:query(&quot;select * from v_files where object ilike 'Product' and objectid in (select id from str_product where not deleted and true and (parentid is null or parentid in (select id from str_product where not deleted and true) )) and objectid != 705 and objectid &lt; 5181&quot;)">
          <productfile>
            <productfileid m:left="-20" m:top="162">
              <xsl:value-of select="id" />
            </productfileid>
            <productid m:left="-20" m:top="196">
              <xsl:value-of select="objectid" />
            </productid>
            <fileid m:left="-20" m:top="230">
              <xsl:value-of select="id" />
            </fileid>
            <isdefault m:left="-20" m:top="262">
              <xsl:value-of select="isdefault" />
            </isdefault>
          </productfile>
        </xsl:for-each>
      </mrep3>
    </target>
  </xsl:template>
</xsl:stylesheet>